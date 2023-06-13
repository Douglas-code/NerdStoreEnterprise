using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using System;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICatalogoService _catalogoService;

        public CarrinhoController(ICarrinhoService carrinhoService, ICatalogoService catalogoService)
        {
            _carrinhoService = carrinhoService;
            _catalogoService = catalogoService;
        }

        [Route("carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await _carrinhoService.ObterCarrinho());
        }

        [HttpPost]
        [Route("carrinho/adicionar-item")]
        public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoViewModel item)
        {
            var produto = await _catalogoService.ObterPorId(item.ProdutoId);
            ValidarItemCarrinho(produto, item.Quantidade);
            if (!OperacaoValida()) return View("Index", await _carrinhoService.ObterCarrinho());

            item.Nome = produto.Nome;
            item.Valor = produto.Valor;
            item.Imagem = produto.Imagem;

            var resposta = await _carrinhoService.AdicionarItemCarrinho(item);

            if (PossuiErrosResponse(resposta)) return View("Index", await _carrinhoService.ObterCarrinho());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/atualizar-item")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, int quantidade)
        {
            var produto = await _catalogoService.ObterPorId(produtoId);
            ValidarItemCarrinho(produto, quantidade);
            if (!OperacaoValida()) return View("Index", await _carrinhoService.ObterCarrinho());

            var item = new ItemCarrinhoViewModel() { ProdutoId = produtoId, Quantidade = quantidade };
            var resposta = await _carrinhoService.AtualizarItemCarrinho(produtoId, item);

            if (PossuiErrosResponse(resposta)) return View("Index", await _carrinhoService.ObterCarrinho());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/remover-item")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var produto = await _catalogoService.ObterPorId(produtoId);

            if (produto == null)
            {
                AdicionarErroValidacao("Produto inexistente!");
                return View("Index", await _carrinhoService.ObterCarrinho());
            }

            var resposta = await _carrinhoService.RemoverItemCarrinho(produtoId);

            if (PossuiErrosResponse(resposta)) return View("Index", await _carrinhoService.ObterCarrinho());

            return RedirectToAction("Index");
        }

        private void ValidarItemCarrinho(ProdutoViewModel produto, int quantidade)
        {
            if (produto == null) AdicionarErroValidacao("Produto inexistente!");

            if (quantidade < 1) AdicionarErroValidacao($"Escolha ao menos uma unidado do produto {produto.Nome}");
            if (quantidade > produto.QuantidadeEstoque) AdicionarErroValidacao($"O produto {produto.Nome} possui {produto.QuantidadeEstoque} unidades");
        }
    }
}
