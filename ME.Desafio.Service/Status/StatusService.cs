using System;
using System.Linq;
using ME.Desafio.Database;
using ME.Desafio.Database.Entities;
using ME.Desafio.Database.Enums;
using ME.Desafio.Model.Status;
using Microsoft.EntityFrameworkCore;

namespace ME.Desafio.Service.Status
{
    public class StatusService : IStatusService
    {
        private readonly DesafioDbContext _context;
        public StatusService(DesafioDbContext context)
        {
            _context = context;
        }

        public StatusResponseModel ChangeStatus(StatusRequestModel model)
        {
            StatusResponseModel responseModel = null;
            var entity = _context.Pedidos.Include("Itens").FirstOrDefault(x => x.NumPedido.Equals(model.Pedido));

            var enumStatus = this.StatusRequestValido(model);

            if (entity != null)
            {
                switch (enumStatus)
                {
                    case EnumStatusRequest.APROVADO:
                        responseModel = this.ValidarPedidoAprovado(model, entity);
                        break;
                    case EnumStatusRequest.REPROVADO:
                        responseModel = this.SetPedidoReprovado(model, entity);
                        break;
                }
            }
            else
            {
                responseModel = this.SetPedidoNaoLocalizado(model);
            }

            return responseModel;
        }

        private StatusResponseModel ValidarPedidoAprovado(StatusRequestModel model, PedidoEntity entity)
        {
            var statusResponseModel = new StatusResponseModel();
            statusResponseModel.Pedido = entity.NumPedido;

            var valorTotalPedido = this.GetValorTotalPedido(entity);
            var quantidadeItensPedido = this.GetQuantidadeTotalItensPedido(entity);

            var isQtdItenAprovadosCorreto = model.ItensAprovados == quantidadeItensPedido;
            var isValorAprovadoExato = model.ValorAprovado == valorTotalPedido;

            if (isQtdItenAprovadosCorreto && isValorAprovadoExato)
            {
                statusResponseModel.Status.Add(EnumStatusResponse.APROVADO.ToString());
            }
            else
            {
                if (model.ValorAprovado < valorTotalPedido)
                {
                    statusResponseModel.Status.Add(EnumStatusResponse.APROVADO_VALOR_A_MENOR.ToString());
                }

                if (model.ValorAprovado > valorTotalPedido)
                {
                    statusResponseModel.Status.Add(EnumStatusResponse.APROVADO_VALOR_A_MAIOR.ToString());
                }

                if (model.ItensAprovados < quantidadeItensPedido)
                {
                    statusResponseModel.Status.Add(EnumStatusResponse.APROVADO_QTD_A_MENOR.ToString());
                }

                if (model.ItensAprovados > quantidadeItensPedido)
                {
                    statusResponseModel.Status.Add(EnumStatusResponse.APROVADO_QTD_A_MAIOR.ToString());
                }
            }

            return statusResponseModel;
        }

        private StatusResponseModel SetPedidoReprovado(StatusRequestModel model, PedidoEntity entity)
        {
            var statusResponseModel = new StatusResponseModel();
            statusResponseModel.Status.Add(EnumStatusResponse.REPROVADO.ToString());
            return statusResponseModel;
        }

        private StatusResponseModel SetPedidoNaoLocalizado(StatusRequestModel model)
        {
            var statusResponseModel = new StatusResponseModel();
            statusResponseModel.Status.Add(EnumStatusResponse.CODIGO_PEDIDO_INVALIDO.ToString());
            return statusResponseModel;
        }

        private decimal GetValorTotalPedido(PedidoEntity entity)
        {
            return entity.Itens.Sum(item => item.Quantidade * item.PrecoUnitario);
        }

        private int GetQuantidadeTotalItensPedido(PedidoEntity entity)
        {
            return entity.Itens.Sum(item => item.Quantidade);
        }

        private EnumStatusRequest StatusRequestValido(StatusRequestModel model)
        {
            try
            {
                return (EnumStatusRequest) Enum.Parse(typeof(EnumStatusRequest), model.Status);
            }
            catch (Exception ex)
            {
                throw new Exception("Status informado não confere.", ex);
            }
        }
    }
}