﻿using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace NSE.Carrinho.API.Model
{
    public class CarrinhoItem
    {
        public CarrinhoItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }
        public Guid CarrinhoId { get; set; }

        [JsonIgnore]
        public CarrinhoCliente CarrinhoCliente { get; set; }

        internal bool EhValido()
        {
            return new ItemCarrinhoValidation().Validate(this).IsValid;
        }

        internal void AssociarCarrinho(Guid carrinhoid)
        {
            CarrinhoId = carrinhoid;
        }

        internal decimal CalcularValor()
        {
            return Valor * Quantidade;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }

        public class ItemCarrinhoValidation : AbstractValidator<CarrinhoItem>
        {
            public ItemCarrinhoValidation()
            {
                RuleFor(c => c.ProdutoId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do produto inválido");

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("o nome do produto não foi informado");

                RuleFor(c => c.Quantidade)
                    .GreaterThan(0)
                    .WithMessage(item => $"A quantidade miníma para {item.Nome} é 1");

                RuleFor(c => c.Quantidade)
                    .LessThanOrEqualTo(CarrinhoCliente.MAX_QUANTIDADE_ITEM)
                    .WithMessage(item => $"A quantidade máxima do {item.Nome} é {CarrinhoCliente.MAX_QUANTIDADE_ITEM}");

                RuleFor(c => c.Valor)
                    .GreaterThan(0)
                    .WithMessage(item => $"O valor do {item.Nome} precisa ser maior que 0");
            }
        }
    }
}
