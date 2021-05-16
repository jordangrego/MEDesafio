using ME.Desafio.Model.Pedido;
using ME.Desafio.Service.Pedido;
using Microsoft.AspNetCore.Mvc;

namespace ME.Desafio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet("{idPedido}")]
        public IActionResult Get(int idPedido)
        {
            return Ok(this._pedidoService.Select(idPedido));
        }

        [HttpGet]
        public IActionResult GetList(int idPedido)
        {
            return Ok(this._pedidoService.List());
        }

        [HttpPost]
        public IActionResult Post([FromBody] PedidoModel model)
        {
            return Ok(this._pedidoService.Insert(model));
        }

        [HttpPut]
        public IActionResult Put([FromBody] PedidoModel model)
        {
            this._pedidoService.Update(model);
            return Ok();
        }

        [HttpDelete("{idPedido}")]
        public IActionResult Delete(int idPedido)
        {
            this._pedidoService.Delete(idPedido);
            return Ok();
        }
    }
}