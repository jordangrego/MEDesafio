namespace ME.Desafio.Model.Pedido
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Qtd { get; set; }
    }
}