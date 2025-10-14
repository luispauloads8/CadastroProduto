using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ClienteProjeto.Nucleo.Excecoes
{
    public class ValidacaoException : Exception
    {
        public IList<ValidationFailure> Errors { get; private set; }

        public ValidacaoException(ValidationResult validationResult) { Errors = validationResult.Errors; }

        public override string Message => string.Join("\n", Errors.Select(x => x.ErrorMessage));

        public dynamic ErrosProjecao => from erro in Errors
                                        select new
                                        {
                                            propriedade = erro.PropertyName,
                                            mensagem = erro.ErrorMessage,
                                        };
    }
}
