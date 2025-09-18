using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Services
{
    public class PessoaService : IPessoaService
    {
        private IPessoaRepository _pessoaRepositoy;
        private IEnderecoService _enderecoService;
        private IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public PessoaService(IPessoaRepository pessoaRepository, IEnderecoService enderecoService,  IMapper mapper, IEnderecoRepository enderecoRepository)
        {
            _pessoaRepositoy = pessoaRepository;
            _enderecoService = enderecoService;
            _mapper = mapper;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Add(PessoaDTO pessoaDTO)
        {
            await _pessoaRepositoy.EnsureConnectionOpenAsync();
            var pessoaEntity = _mapper.Map<Pessoa>(pessoaDTO);

            if (pessoaDTO.Id is null && pessoaEntity.Endereco != null)
            {
                var endereco = await _enderecoRepository.CreateAsync(pessoaEntity.Endereco);
                pessoaEntity.EnderecoId = endereco.Id;

            }
            
            await _pessoaRepositoy.CreateAsync(pessoaEntity);
        }

        public async Task Delete(int? id)
        {
            await _pessoaRepositoy.EnsureConnectionOpenAsync();
            var pessoaEntity = await _pessoaRepositoy.GetByIdAsync(id);
            await _pessoaRepositoy.DeleteAsync(pessoaEntity);

            if (pessoaEntity.EnderecoId != null) 
            {
                var endereco = await _enderecoRepository.GetByIdAsync(pessoaEntity.EnderecoId);

                if (endereco != null)
                {
                    await _enderecoRepository.DeleteAsync(endereco);
                }
            }
        }

        public async Task<PessoaDTO> GetById(int? id)
        {
            await _pessoaRepositoy.EnsureConnectionOpenAsync();
            var pessoaEntity = await _pessoaRepositoy.GetByIdAsync(id);

            var endereco = await _enderecoService.GetById(pessoaEntity.EnderecoId);

            pessoaEntity.Endereco = _mapper.Map<Endereco>(endereco);

            return _mapper.Map<PessoaDTO>(pessoaEntity);
        }

        public async Task<IEnumerable<PessoaDTO>> GetPessoas()
        {
            await _pessoaRepositoy.EnsureConnectionOpenAsync();
            var pessoaEntity = await _pessoaRepositoy.GetPessoaAsync();
            return _mapper.Map<IEnumerable<PessoaDTO>>(pessoaEntity);
        }

        public async Task<List<PessoaDTO>> GetPessoaTermo(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return new List<PessoaDTO>();
            }

            var pessoaTermo = await _pessoaRepositoy.GetPessoaTermoAsync(search);

            return _mapper.Map<List<PessoaDTO>>(pessoaTermo);
        }

        public async Task Update(PessoaDTO pessoaDTO)
        {
            await _pessoaRepositoy.EnsureConnectionOpenAsync();
            var pessoaEntity = _mapper.Map<Pessoa>(pessoaDTO);

            await _pessoaRepositoy.UpdateAsync(pessoaEntity);
        }
    }
}
