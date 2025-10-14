using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Validator
{
    public abstract class ValidadorAbstratoCadastro<T> : ValidadorAbstrato<T> 
        where T : class
    {
        public abstract void AssineRegrasInsercao();
        public abstract void AssineRegrasAtualizacao();
        public abstract void AssineRegrasExclusao();
    }
}
