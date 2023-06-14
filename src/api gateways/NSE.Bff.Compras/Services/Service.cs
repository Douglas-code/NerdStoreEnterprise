using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.Bff.Compras.Services
{
    public abstract class Service
    {
        protected StringContent ObterConteudo(object dado)
        {
            return new StringContent(JsonSerializer.Serialize(dado), Encoding.UTF8, "application/json");
        }

        protected async Task<T> DesearilizarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            if(response.StatusCode == System.Net.HttpStatusCode.BadRequest) return false;

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
