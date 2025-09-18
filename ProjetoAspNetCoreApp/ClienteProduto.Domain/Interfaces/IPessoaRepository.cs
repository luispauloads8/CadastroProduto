using ClienteProjeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Domain.Interfaces
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> GetPessoaAsync();
        Task<Pessoa> GetByIdAsync(int? id);
        Task<List<Pessoa>> GetPessoaTermoAsync(string search);
        Task<Pessoa> CreateAsync(Pessoa pessoa);
        Task<Pessoa> UpdateAsync(Pessoa pessoa);
        Task<Pessoa> DeleteAsync(Pessoa pessoa);
        Task EnsureConnectionOpenAsync();
    }
}
