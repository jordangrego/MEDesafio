using System.Collections.Generic;

namespace ME.Desafio.Database.Entities
{
    public class ItemEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }

        public virtual int PedidoId { get; set; }
        public virtual PedidoEntity Pedido { get; set; }
    }
}