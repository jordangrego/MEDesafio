using System.Collections.Generic;

namespace ME.Desafio.Model.Status
{
    public class StatusResponseModel
    {
        public StatusResponseModel()
        {
            this.Status = new List<string>();
        }

        public string Pedido { get; set; }
        public List<string> Status { get; set; }
    }
}