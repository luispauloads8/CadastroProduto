using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Services
{
    public class EnderecoService : IEnderecoService
    {
        private IEnderecoRepository _enderecoRepository;
        private ICidadeRepository _cidadeRepository;
        private readonly IMapper _mapper;

        public EnderecoService(IEnderecoRepository enderecoRepository, ICidadeRepository cidadeRepository, IMapper mapper)
        {
            _enderecoRepository = enderecoRepository;
            _cidadeRepository = cidadeRepository;
            _mapper = mapper;
        }

        public Task Add(EnderecoDTO enderecoDTO)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<EnderecoDTO> GetById(int? id)
        {
            await _enderecoRepository.EnsureConnectionOpenAsync();
            var enderecoEntity = await _enderecoRepository.GetByIdAsync(id);

            var cidade = await _cidadeRepository.GetByIdAsync(enderecoEntity.CidadeId);
            enderecoEntity.Cidade = cidade;

            return _mapper.Map<EnderecoDTO>(enderecoEntity);
        }

        public Task<IEnumerable<EnderecoDTO>> GetEnderecos()
        {
            throw new NotImplementedException();
        }

        public Task<List<EnderecoDTO>> GetEnderecoTermo(string search)
        {
            throw new NotImplementedException();
        }

        public Task Update(EnderecoDTO enderecoDTO)
        {
            throw new NotImplementedException();
        }
    }
}
