using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.Identidade.API.Controllers;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Controllers
{
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientesController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(new RegistrarClienteCommand(System.Guid.NewGuid(), "Testenjasvjdasjdasdlasda", "meuemail@gmail.com", "68070291010")));
        }
    }
}
