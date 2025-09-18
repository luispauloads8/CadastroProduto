using ClienteProjeto.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Interfaces
{
    public interface IPessoaService
    {
        Task<IEnumerable<PessoaDTO>> GetPessoas();
        Task<PessoaDTO> GetById(int? id);
        Task<List<PessoaDTO>> GetPessoaTermo(string search);
        Task Add(PessoaDTO pessoaDTO);
        Task Update(PessoaDTO pessoaDTO);
        Task Delete(int? id);
    }
}
