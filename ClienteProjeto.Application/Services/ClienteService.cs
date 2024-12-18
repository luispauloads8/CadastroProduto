using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
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

    public async Task Add(ClienteDTO clienteDTO)
    {
        await _clienteRepository.EnsureConnectionOpenAsync();
        var clienteEntity = _mapper.Map<Cliente>(clienteDTO);
        await _clienteRepository.CreateAsync(clienteEntity);
    }

    public async Task Delete(int? id)
    {
        await _clienteRepository.EnsureConnectionOpenAsync();
        var clienteEntity = await _clienteRepository.GetByIdAsync(id);
        await _clienteRepository.DeleteAsync(clienteEntity);
    }

    public async Task<ClienteDTO> GetById(int? id)
    {
        await _clienteRepository.EnsureConnectionOpenAsync();
        var clienteEntity = await _clienteRepository.GetByIdAsync(id);
        return _mapper.Map<ClienteDTO>(clienteEntity);
    }

    public async Task<IEnumerable<ClienteDTO>> GetClientes()
    {
        await _clienteRepository.EnsureConnectionOpenAsync();
        var clienteEntity = await _clienteRepository.GetClienteAsync();
        return _mapper.Map<IEnumerable<ClienteDTO>>(clienteEntity); 
    }

    public async Task Update(ClienteDTO clienteDTO)
    {
        await _clienteRepository.EnsureConnectionOpenAsync();
        var clienteEntity = _mapper.Map<Cliente>(clienteDTO);
        await _clienteRepository.UpdateAsync(clienteEntity);

    }

}
