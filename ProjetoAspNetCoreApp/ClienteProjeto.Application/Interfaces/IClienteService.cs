using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<ClienteDTO>> GetClientes();
    Task<ClienteDTO> GetById(int? id);
    Task<List<ClienteDTO>> GetClienteTermo(string search);
    Task Add(ClienteDTO clienteDTO);
    Task Update(ClienteDTO clienteDTO);
    Task Delete(int? id);
}
