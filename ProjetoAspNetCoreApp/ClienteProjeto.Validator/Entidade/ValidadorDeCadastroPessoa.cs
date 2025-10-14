using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Enum;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Validator.Entidade
{
    public class ValidadorDeCadastroPessoa : ValidadorAbstrato<PessoaDTO>
    {
        public ValidadorDeCadastroPessoa()
        {
            // Validação comum para todas as pessoas
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.");

            When(x => x.TipoPessoa == TipoPessoa.Fisica, () =>
            {
                RuleFor(x => x)
                    .Custom((x, context) =>
                    {
                        var pf = x as PessoaFisicaDTO;
                        if (pf == null) return; // segurança

                        if (string.IsNullOrWhiteSpace(pf.CPF))
                            context.AddFailure("CPF", "O CPF é obrigatório.");
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(pf.CPF, @"^\d{11}$"))
                            context.AddFailure("CPF", "O CPF deve conter 11 dígitos.");
                    });
            });


            When(x => x.TipoPessoa == TipoPessoa.Juridica, () =>
            {
                RuleFor(x => x)
                    .Custom((x, context) =>
                    {
                        var pj = x as PessoaJuridicaDTO;
                        if (pj == null) return;

                        if (string.IsNullOrWhiteSpace(pj.CNPJ))
                            context.AddFailure("CNPJ", "O CNPJ é obrigatório.");
                        else if (!System.Text.RegularExpressions.Regex.IsMatch(pj.CNPJ, @"^\d{14}$"))
                            context.AddFailure("CNPJ", "O CNPJ deve conter 14 dígitos.");
                    });
            });



        }
    }

}
