using NSE.Core.Comunication;
using NSE.WebApp.MVC.Models;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Options;
using System.Net.Http;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Services
{
    public class ComprasBffService : Service, IComprasBffService
    {
        private readonly HttpClient _httpClient;

        public ComprasBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ComprasBffUrl);
        }


        public async Task<CarrinhoViewModel> ObterCarrinho()
        {
            var response = await _httpClient.GetAsync("/compras/carrinho/");

            TratarErrosResponse(response);

            return await DesearilizarObjetoResponse<CarrinhoViewModel>(response);
        }

        public async Task<int> ObterQuantidadeCarrinho()
        {
            var response = await _httpClient.GetAsync("/compras/carrinho-quantidade/");

            TratarErrosResponse(response);

            return await DesearilizarObjetoResponse<int>(response);
        }

        public async Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel produto)
        {
            var itemContent = ObterConteudo(produto);

            var response = await _httpClient.PostAsync("/compras/carrinho/items", itemContent);

            if (!TratarErrosResponse(response)) return await DesearilizarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoViewModel produto)
        {
            var itemContent = ObterConteudo(produto);

            var response = await _httpClient.PutAsync($"/compras/carrinho/items/{produto.ProdutoId}", itemContent);

            if (!TratarErrosResponse(response)) return await DesearilizarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }

        public async Task<ResponseResult> RemoverItemCarrinho(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"/compras/carrinho/items/{produtoId}");

            if (!TratarErrosResponse(response)) return await DesearilizarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
    }
}
