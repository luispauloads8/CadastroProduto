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

namespace ClienteProjeto.Application.Services;

public class ContaContabilService : IContaContabilService
{
    private IContaContabilRepository _contaContabilRepository;
    private IGrupoContaRepository _grupoContaRepository;
    private readonly IMapper _mapper;

    public ContaContabilService(IContaContabilRepository contaContabilRepository, IGrupoContaRepository grupoContaRepository, IMapper mapper)
    {
        _contaContabilRepository = contaContabilRepository;
        _grupoContaRepository = grupoContaRepository;
        _mapper = mapper;
    }

    public async Task Add(ContaContabilDTO contaContabilDTO)
    {
        await _contaContabilRepository.EnsureConnectionOpenAsync();
        var contaContabilEntity = _mapper.Map<ContaContabil>(contaContabilDTO);
        await _contaContabilRepository.CreateAsync(contaContabilEntity);

    }

    public async Task Delete(int? id)
    {
        await _contaContabilRepository.EnsureConnectionOpenAsync();
        var contaContabilEntity = await _contaContabilRepository.GetByIdAsync(id);
        await _contaContabilRepository.DeleteAsync(contaContabilEntity);
    }

    public async Task<ContaContabilDTO> GetById(int? id)
    {
        await _contaContabilRepository.EnsureConnectionOpenAsync();
        var contaContabil = await _contaContabilRepository.GetByIdAsync(id);

        var grupoConta = await _grupoContaRepository.GetByIdAsync(contaContabil.GrupoContaId);
        contaContabil.GrupoConta = grupoConta;

        return _mapper.Map<ContaContabilDTO>(contaContabil);
    }

    public async Task<IEnumerable<ContaContabilDTO>> GetContaContabeis()
    {
        await _contaContabilRepository.EnsureConnectionOpenAsync();
        var contasContabeis = await _contaContabilRepository.GetClienteAsync();
        return _mapper.Map<IEnumerable<ContaContabilDTO>>(contasContabeis);
    }

    public async Task Update(ContaContabilDTO contaContabilDTO)
    {
        await _contaContabilRepository.EnsureConnectionOpenAsync();
        var contaContabilEntity = _mapper.Map<ContaContabil>(contaContabilDTO);
        await _contaContabilRepository.UpdateAsync(contaContabilEntity);
    }

}
