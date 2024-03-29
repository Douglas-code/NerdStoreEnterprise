﻿using NSE.Core.Comunication;
using NSE.WebApp.MVC.Models;
using System.Threading.Tasks;
using System;

namespace NSE.WebApp.MVC.Services
{
    public interface IComprasBffService
    {
        Task<CarrinhoViewModel> ObterCarrinho();
        Task<int> ObterQuantidadeCarrinho();
        Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel produto);
        Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoViewModel produto);
        Task<ResponseResult> RemoverItemCarrinho(Guid produtoId);
    }
}
