using System.Collections.Generic;

namespace ME.Desafio.Database.Entities
{
    public class PedidoEntity
    {
        public int Id { get; set; }
        public string NumPedido { get; set; }
        public List<ItemEntity> Itens { get; set; }
    }
}