﻿using Microsoft.AspNetCore.Mvc;
using NSE.Core.Comunication;
using NSE.WebApp.MVC.Models;
using System.Linq;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool PossuiErrosResponse(ResponseResult response)
        {
            if(response != null && response.Errors.Mensagens.Any())
            {
                foreach(var mensagem in response.Errors.Mensagens)
                {
                    ModelState.AddModelError(string.Empty, mensagem);
                }

                return true;
            }

            return false;
        }

        protected void AdicionarErroValidacao(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        }

        protected bool OperacaoValida()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}
