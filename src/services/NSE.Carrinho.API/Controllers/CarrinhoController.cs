using Microsoft.AspNetCore.Authorization;
using NSE.Identidade.API.Controllers;

namespace NSE.Carrinho.API.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
    }
}
