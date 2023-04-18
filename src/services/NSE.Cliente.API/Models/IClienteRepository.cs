using NSE.Core.DomainObjects;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Models
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        void Adicionar(Cliente cliente);
        Task<Cliente> ObterPorCpf(string cpf);
    }
}
