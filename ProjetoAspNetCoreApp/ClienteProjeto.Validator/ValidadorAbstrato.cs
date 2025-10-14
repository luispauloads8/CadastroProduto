using ClienteProjeto.Nucleo.Excecoes;
using FluentValidation;

namespace ClienteProjeto.Validator
{
    public abstract class ValidadorAbstrato<T> : AbstractValidator<T>
        where T : class
    {
        public bool Valide(T instancia)
        {
            var resultadoDaValidacao = Validate(instancia);
            if (!resultadoDaValidacao.IsValid) throw new ValidacaoException(resultadoDaValidacao);
            return true;
        }

        public async Task<bool> ValideAsync(T instancia)
        {
            var resultadoDaValidacao = await ValidateAsync(instancia);
            if (!resultadoDaValidacao.IsValid) throw new ValidacaoException(resultadoDaValidacao);

            return true;
        }
    }
}
