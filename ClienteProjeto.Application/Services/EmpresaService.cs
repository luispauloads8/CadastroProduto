using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class EmpresaService : IEmpresaService
{
    private IEmpresaRepository _empresaRepository;
    private readonly IMapper _mapper;

    public EmpresaService(IEmpresaRepository empresaRepository, IMapper mapper)
    {
        _empresaRepository = empresaRepository;
        _mapper = mapper;
    }

    public async Task Add(EmpresaDTO empresaDTO)
    {
        await _empresaRepository.EnsureConnectionOpenAsync();
        var empresaEntity = _mapper.Map<Empresa>(empresaDTO);
        await _empresaRepository.CreateAsync(empresaEntity);

    }

    public async Task Delete(int? id)
    {
        await _empresaRepository.EnsureConnectionOpenAsync();
        var empresaEntity = _empresaRepository.GetByIdAsync(id).Result;
        await _empresaRepository.DeleteAsync(empresaEntity);
    }

    public async Task<EmpresaDTO> GetById(int? id)
    {
        await _empresaRepository.EnsureConnectionOpenAsync();
        var empresaEntity = await _empresaRepository.GetByIdAsync(id);
        return _mapper.Map<EmpresaDTO>(empresaEntity);
    }

    public async Task<IEnumerable<EmpresaDTO>> GetEmpresas()
    {
        await _empresaRepository.EnsureConnectionOpenAsync();
        var empresaEntity = await _empresaRepository.GetEmpresaAsync();
        return _mapper.Map<IEnumerable<EmpresaDTO>>(empresaEntity);
    }

    public async Task Update(EmpresaDTO empresaDTO)
    {
        await _empresaRepository.EnsureConnectionOpenAsync();
        var empresaEntity = _mapper.Map<Empresa>(empresaDTO);
        await _empresaRepository.UpdateAsync(empresaEntity);

    }
}
