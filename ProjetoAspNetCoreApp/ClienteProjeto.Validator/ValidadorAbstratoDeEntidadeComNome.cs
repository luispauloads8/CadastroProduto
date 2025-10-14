using ClienteProjeto.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Validator
{
    public class ValidadorAbstratoDeEntidadeComNome<T> : ValidadorAbstratoCadastro<T>
        where T : EntidadeComNome
    {
        public override void AssineRegrasAtualizacao()
        {
            AssineRegrasComumInsercaoEAtualizacao();
        }

        public override void AssineRegrasExclusao()
        {
            throw new NotImplementedException();
        }

        public override void AssineRegrasInsercao()
        {
            AssineRegrasComumInsercaoEAtualizacao();
        }

        protected virtual void AssineRegrasComumInsercaoEAtualizacao()
        {
            RuleFor(entidade => entidade.Nome)
                .NotNull()
                .WithName("Nome")
                .WithMessage(MensagensDeInconsistenciasValidacao.ATRIBUTO_OBRIGATORIO_NAO_INFORMADO);
        }
    }
}
