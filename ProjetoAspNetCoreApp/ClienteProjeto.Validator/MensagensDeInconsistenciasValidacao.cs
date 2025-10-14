using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Validator
{
    public static class MensagensDeInconsistenciasValidacao
    {
        public const string ATRIBUTO_OBRIGATORIO_NAO_INFORMADO = "O atributo {PropertyName} é obrigatório!";

        public const string ATRIBUTO_SIGLA_INCORRETO = "A sigla deve possuir somente letras e ou números, exemplo: ABC123 ou ABC.123!";

        public const string ATRIBUTO_SIGLA_JA_UTILIZADO = "A sigla já está sendo utilizada em outro item, escolha outra!";
    }
}
