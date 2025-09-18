using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Nucleo.Excecoes
{
    public class InconsistenciaException : Exception
    {
        public InconsistenciaException(string mensagem) : base(mensagem) { }
    }
}
