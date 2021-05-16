using System.Collections.Generic;

namespace ME.Desafio.Model.Pedido
{
    public class PedidoModel
    {
        public PedidoModel()
        {
            this.Itens = new List<ItemModel>();
        }

        public int IdPedido { get; set; }
        public string Pedido { get; set; }
        public List<ItemModel> Itens { get; set; }
    }
}