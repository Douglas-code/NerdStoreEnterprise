﻿using Microsoft.Extensions.Options;
using NSE.Core.Comunication;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class AutenticacaoService : Service, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;

        public AutenticacaoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);
            _httpClient = httpClient;
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = ObterConteudo(usuarioLogin);
            var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin()
                {
                    ResponseResult = await DesearilizarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DesearilizarObjetoResponse<UsuarioRespostaLogin>(response);
        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var loginContent = ObterConteudo(usuarioRegistro);
            var response = await _httpClient.PostAsync("/api/identidade/nova-conta", loginContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin()
                {
                    ResponseResult = await DesearilizarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DesearilizarObjetoResponse<UsuarioRespostaLogin>(response);
        }
    }
}
