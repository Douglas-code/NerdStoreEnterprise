using NSE.Bff.Compras.Models;
using System;
using System.Threading.Tasks;

namespace NSE.Bff.Compras.Services
{
    public interface ICatalogoService
    {
        Task<ItemProdutoDTO> ObterPorId(Guid id);
    }
}
