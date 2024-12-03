using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class ClienteService : IClienteService
{
    private IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public Task Add(ClienteDTO clienteDTO)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<ClienteDTO> GetById(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ClienteDTO>> GetClientes()
    {
        throw new NotImplementedException();
    }

    public Task Update(ClienteDTO clienteDTO)
    {
        throw new NotImplementedException();
    }

}
