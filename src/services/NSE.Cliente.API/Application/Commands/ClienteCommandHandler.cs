using FluentValidation.Results;
using MediatR;
using NSE.Clientes.API.Models;
using NSE.Core.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(RegistrarClienteCommand request, CancellationToken cancellationToken)
        {
            if (request.EhValido()) return request.ValidationResult;

            var cliente = new Cliente(request.Id, request.Nome, request.Email, request.Cpf);

            if (true) // Já existe cliente com o cpf informado
            {
                AdicionarErro("Cliente já existe!");
                return ValidationResult;
            }

            return request.ValidationResult;
        }
    }
}
