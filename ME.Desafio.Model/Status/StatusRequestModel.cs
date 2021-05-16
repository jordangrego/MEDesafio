namespace ME.Desafio.Model.Status
{
    public class StatusRequestModel
    {
        public string Status { get; set; }
        public int ItensAprovados { get; set; }
        public int ValorAprovado { get; set; }
        public string Pedido { get; set; }
    }
}