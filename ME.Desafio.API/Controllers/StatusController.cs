using ME.Desafio.Model.Status;
using ME.Desafio.Service.Status;
using Microsoft.AspNetCore.Mvc;

namespace ME.Desafio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] StatusRequestModel model)
        {
            return Ok(this._statusService.ChangeStatus(model));
        }
    }
}