using Microsoft.AspNetCore.Authorization;
using NSE.Identidade.API.Controllers;

namespace NSE.Clientes.API.Controllers
{
    [Authorize]
    public class ClientesController : MainController
    {
    }
}
