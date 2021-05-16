using System.Collections.Generic;
using ME.Desafio.Model.Pedido;

namespace ME.Desafio.Service.Pedido
{
    public interface IPedidoService
    {
        PedidoModel Select(int idPedido);
        List<PedidoModel> List();
        int Insert(PedidoModel pedido);
        void Update(PedidoModel pedido);
        void Delete(int idPedido);
    }
}