using ClienteProjeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Domain.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> GetEnderecoAsync();
        Task<Endereco> GetByIdAsync(int? id);
        Task<List<Endereco>> GetEnderecoTermoAsync(string search);
        Task<Endereco> CreateAsync(Endereco endereco);
        Task<Endereco> UpdateAsync(Endereco endereco);
        Task<Endereco> DeleteAsync(Endereco endereco);
        Task EnsureConnectionOpenAsync();
    }
}
