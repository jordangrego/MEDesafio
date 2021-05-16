using System;
using System.Collections.Generic;
using System.Linq;
using ME.Desafio.Database;
using ME.Desafio.Database.Entities;
using ME.Desafio.Model.Pedido;
using Microsoft.EntityFrameworkCore;

namespace ME.Desafio.Service.Pedido
{
    public class PedidoService : IPedidoService
    {
        private readonly DesafioDbContext _context;
        public PedidoService(DesafioDbContext context)
        {
            _context = context;
        }

        public PedidoModel Select(int idPedido)
        {
            var entity = _context.Pedidos.Include("Itens").FirstOrDefault(x => x.Id == idPedido);
            var model = new PedidoModel()
            {
                Pedido = entity.NumPedido,
                IdPedido = entity.Id,
                Itens = entity.Itens != null ? entity.Itens.Select(i => new ItemModel()
                {
                Id = i.Id,
                Descricao = i.Descricao,
                PrecoUnitario = i.PrecoUnitario,
                Qtd = i.Quantidade,
                }).ToList() : null
            };

            return model;
        }

        public List<PedidoModel> List()
        {
            return _context.Pedidos.Include("Itens").ToList().Select(p => new PedidoModel()
            {
                IdPedido = p.Id,
                    Pedido = p.NumPedido,
                    Itens = p.Itens != null ? p.Itens.Select(i => new ItemModel()
                    {
                        Id = i.Id,
                            Descricao = i.Descricao,
                            PrecoUnitario = i.PrecoUnitario,
                            Qtd = i.Quantidade,
                    }).ToList() : null
            }).ToList();
        }

        public int Insert(PedidoModel model)
        {
            bool pedidoExists = _context.Pedidos.Where(x => x.NumPedido == model.Pedido).Any();

            if (!pedidoExists)
            {
                if (model.Itens == null || model.Itens.Count == 0)
                {
                    throw new Exception("Não é possível inserir um pedido sem itens.");
                }

                PedidoEntity pedido = new PedidoEntity()
                {
                    NumPedido = model.Pedido,
                    Itens = model.Itens.Select(x => new ItemEntity()
                    {
                    Descricao = x.Descricao,
                    PrecoUnitario = x.PrecoUnitario,
                    Quantidade = x.Qtd,
                    }).ToList()
                };

                _context.Pedidos.Add(pedido);
                _context.SaveChanges();
                return pedido.Id;
            }
            else
            {
                throw new Exception("Pedido já existe!");
            }
        }

        public void Update(PedidoModel model)
        {
            bool pedidoExists = _context.Pedidos.Where(x => x.NumPedido == model.Pedido).Any();

            if (pedidoExists)
            {
                var pedido = _context.Pedidos.Where(x => x.NumPedido == model.Pedido).FirstOrDefault();
                var lstItensRemocao = _context.Itens.Where(x => x.PedidoId == pedido.Id);

                foreach (ItemEntity entity in lstItensRemocao)
                {
                    _context.Itens.Remove(entity);
                }

                foreach (ItemModel itemModel in model.Itens)
                {
                    ItemEntity itemEntity = new ItemEntity()
                    {
                        Descricao = itemModel.Descricao,
                        PrecoUnitario = itemModel.PrecoUnitario,
                        Quantidade = itemModel.Qtd,
                        PedidoId = pedido.Id
                    };

                    _context.Itens.Add(itemEntity);
                }

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Pedido não existe!");
            }
        }

        public void Delete(int idPedido)
        {
            bool pedidoExists = _context.Pedidos.Where(x => x.Id == idPedido).Any();

            if (pedidoExists)
            {
                var pedido = _context.Pedidos.Where(x => x.Id == idPedido).FirstOrDefault();
                _context.Pedidos.Remove(pedido);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Pedido não existe!");
            }
        }
    }
}