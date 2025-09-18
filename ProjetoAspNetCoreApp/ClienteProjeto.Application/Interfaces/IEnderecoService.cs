using ClienteProjeto.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Interfaces
{
    public interface IEnderecoService
    {
        Task<IEnumerable<EnderecoDTO>> GetEnderecos();
        Task<EnderecoDTO> GetById(int? id);
        Task<List<EnderecoDTO>> GetEnderecoTermo(string search);
        Task Add(EnderecoDTO enderecoDTO);
        Task Update(EnderecoDTO enderecoDTO);
        Task Delete(int? id);
    }
}
