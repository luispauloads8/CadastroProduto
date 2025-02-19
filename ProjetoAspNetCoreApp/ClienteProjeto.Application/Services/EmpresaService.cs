using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class EmpresaService : IEmpresaService
{
    private IEmpresaRepository _empresaRepository;
    private ICidadeRepository _cidadeRepository;
    private readonly IMapper _mapper;

    public EmpresaService(IEmpresaRepository empresaRepository, ICidadeRepository cidadeRepository, IMapper mapper)
    {
        _empresaRepository = empresaRepository;
        _cidadeRepository = cidadeRepository;
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
        var empresaEntity = await _empresaRepository.GetByIdAsync(id);
        await _empresaRepository.DeleteAsync(empresaEntity);
    }

    public async Task<EmpresaDTO> GetById(int? id)
    {
        await _empresaRepository.EnsureConnectionOpenAsync();
        var empresaEntity = await _empresaRepository.GetByIdAsync(id);

        var cidade = await _cidadeRepository.GetByIdAsync(empresaEntity.CidadeId);
        empresaEntity.Cidade = cidade;

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

    public async Task<List<EmpresaDTO>> GetEmpresaTermo(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
        {
            return new List<EmpresaDTO>();
        }

        var empresaTermo = await _empresaRepository.GetEmpresaTermoAsync(termo);

        return _mapper.Map<List<EmpresaDTO>>(empresaTermo);

    }
}
