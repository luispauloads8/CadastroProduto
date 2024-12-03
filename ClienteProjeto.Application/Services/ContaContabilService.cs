using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
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
    private readonly IMapper _mapper;

    public ContaContabilService(IContaContabilRepository contaContabilRepository, IMapper mapper)
    {
        _contaContabilRepository = contaContabilRepository;
        _mapper = mapper;
    }

    public Task Add(ContaContabilDTO contaContabilDTO)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<ContaContabilDTO> GetById(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ContaContabilDTO>> GetContaContabeis()
    {
        throw new NotImplementedException();
    }

    public Task Update(ContaContabilDTO contaContabilDTO)
    {
        throw new NotImplementedException();
    }

    public Task EnsureConnectionOpenAsync()
    {
        throw new NotImplementedException();
    }
}
