using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class ClienteService : IClienteService
{
    private IClienteRepository _clienteRepository;
    private ICidadeRepository _cidadeRepository;
    private IPessoaRepository _pessoaRepository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository clienteRepository, ICidadeRepository cidadeRepository, IPessoaRepository pessoaRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _cidadeRepository = cidadeRepository;
        _pessoaRepository = pessoaRepository;
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

        var pessoa = await _pessoaRepository.GetByIdAsync(clienteEntity.PessoaId);
        clienteEntity.Pessoa = pessoa;
            

        return _mapper.Map<ClienteDTO>(clienteEntity);
    }

    public async Task<IEnumerable<ClienteDTO>> GetClientes()
    {
        await _clienteRepository.EnsureConnectionOpenAsync();
        var clienteEntity = await _clienteRepository.GetClienteAsync();

        await FiltraPessoa(clienteEntity);

        return _mapper.Map<IEnumerable<ClienteDTO>>(clienteEntity);
    }

    private async Task FiltraPessoa(IEnumerable<Cliente> clienteEntity)
    {
        var pessoasReposity = await _pessoaRepository.GetPessoaAsync();

        foreach (var cliente in clienteEntity)
        {
            foreach (var pessoa in pessoasReposity)
            {
                if (cliente.PessoaId == pessoa.Id)
                {
                    cliente.Pessoa = pessoa;
                }
            }
        }
    }

    public async Task Update(ClienteDTO clienteDTO)
    {
        await _clienteRepository.EnsureConnectionOpenAsync();
        var clienteEntity = _mapper.Map<Cliente>(clienteDTO);
        await _clienteRepository.UpdateAsync(clienteEntity);

    }

    public async Task<List<ClienteDTO>> GetClienteTermo(string search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return new List<ClienteDTO>();
        }

        var clienteTermo = await _clienteRepository.GetClienteTermoAsync(search);

        var pessoaRepository = await _pessoaRepository.GetPessoaAsync();

        foreach(var pessoa in pessoaRepository)
        {
            foreach(var cliente in clienteTermo)
            {
                if (cliente.PessoaId == pessoa.Id)
                {
                    cliente.Pessoa = pessoa;
                }
            }
        }

        return _mapper.Map<List<ClienteDTO>>(clienteTermo);
    }

    public async Task<PagedResult<ClienteDTO>> ObterPaginadoAsync(int pageNumber, int pageSize)
    {
        // Aqui pode entrar regra de negócio, validação, logs, etc
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var clienteEntity = await _clienteRepository.GetPagedAsync(pageNumber, pageSize);

        //await FiltraPessoa(clienteEntity.Items);

        return _mapper.Map<PagedResult<ClienteDTO>>(clienteEntity);
    }
}
