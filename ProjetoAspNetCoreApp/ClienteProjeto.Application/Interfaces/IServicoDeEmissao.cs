using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Interfaces
{
    public interface IServicoDeEmissao<TEntidade>
    {
        Task<byte[]> Gerar(TEntidade entidade);
    }
}
