using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Identidade.API.Controllers;
using System.Threading.Tasks;

namespace NSE.Bff.Compras.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        [HttpGet("compras/carrinho")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse();
        }
    }
}
