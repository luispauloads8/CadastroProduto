using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class LancamentoService : ILancamentoService
{
    private ILancamentoRepository _lancamentoRepository;
    private IProdutoServicoRepository _produtoServicoRepository;
    private IItensLancamentosRepository _itensLancamentosRepository;
    private IEmpresaRepository _empresaRespository;
    private IClienteRepository _clienteRepository;
    private IContaContabilRepository _contaContabilRepository;
    private readonly IMapper _mapper;

    public LancamentoService(ILancamentoRepository lancamentoRepository, IMapper mapper, IProdutoServicoRepository produtoServicoRepository, 
        IItensLancamentosRepository itensLancamentosRepository, IEmpresaRepository empresaRepository, IClienteRepository clienteRepository, IContaContabilRepository contaContabilRepository)
    {
        _lancamentoRepository = lancamentoRepository;
        _mapper = mapper;
        _produtoServicoRepository = produtoServicoRepository;
        _itensLancamentosRepository = itensLancamentosRepository;
        _empresaRespository = empresaRepository;
        _clienteRepository = clienteRepository;
        _contaContabilRepository = contaContabilRepository;
    }

    public  async Task Add(LancamentoDTO lancamentoDTO)
    {
        await _lancamentoRepository.EnsureConnectionOpenAsync();
        var lancamentoEntity = _mapper.Map<Lancamento>(lancamentoDTO);
        await _lancamentoRepository.CreateAsync(lancamentoEntity);
    }

    public async Task Delete(int? id)
    {
        await _lancamentoRepository.EnsureConnectionOpenAsync();
        var lancamentoEntity = await _lancamentoRepository.GetByIdAsync(id);
        await _lancamentoRepository.DeleteAsync(lancamentoEntity);
    }

    public async Task<LancamentoDTO> GetById(int? id)
    {
        await _lancamentoRepository.EnsureConnectionOpenAsync();
        var lancamentoEntity = await _lancamentoRepository.GetByIdAsync(id);

        var empresaLancamento = await _empresaRespository.GetByIdAsync(lancamentoEntity.EmpresaId);
        lancamentoEntity.Empresa = empresaLancamento;

        var produtoServico = await _produtoServicoRepository.GetByIdAsync(lancamentoEntity.ProdutoServicoId);
        lancamentoEntity.ProdutoServico = produtoServico;

        var cliente = await _clienteRepository.GetByIdAsync(lancamentoEntity.ClienteId);
        lancamentoEntity.Cliente = cliente;

        var contaContabil = await _contaContabilRepository.GetByIdAsync(lancamentoEntity.ContaContabilId);
        lancamentoEntity.ContaContabil = contaContabil;

        var itenLancamento = await _itensLancamentosRepository.GetByIdAsync(lancamentoEntity.Id);
        lancamentoEntity.ItensLancamentos ??= new List<ItensLancamento>();
        lancamentoEntity.ItensLancamentos.Add(itenLancamento);


        return _mapper.Map<LancamentoDTO>(lancamentoEntity);
    }


    public async Task<IEnumerable<LancamentoDTO>> GetLancamentos()
    {
        await _lancamentoRepository.EnsureConnectionOpenAsync();
        var lancamentosEntity = await _lancamentoRepository.GetLancamentoAsync();

        var cliente = new Cliente();

        // Dicionário para armazenar ProdutoServico por ID
        var produtoServicoDictionary = new Dictionary<int, ProdutoServico>();

        foreach (var itemProduto in lancamentosEntity)
        {
            if (!produtoServicoDictionary.ContainsKey(itemProduto.ProdutoServicoId))
            {
                // Obter ProdutoServico e adicionar ao dicionário
                var produtoServico = await _produtoServicoRepository.GetByIdAsync(itemProduto.ProdutoServicoId);
                produtoServicoDictionary[itemProduto.ProdutoServicoId] = produtoServico;

                //recupera cliente
                var clienteReposistory = await _clienteRepository.GetByIdAsync(itemProduto.ClienteId);
                cliente = clienteReposistory;
            }

            

            // Atribuir ProdutoServico correspondente à instância de Lancamento
            itemProduto.ProdutoServico = produtoServicoDictionary[itemProduto.ProdutoServicoId];

            //atribui cliente 
            itemProduto.Cliente = cliente;
        }

        return _mapper.Map<IEnumerable<LancamentoDTO>>(lancamentosEntity);
    }

    public async Task Update(LancamentoDTO lancamentoDTO)
    {
        await _lancamentoRepository.EnsureConnectionOpenAsync();
        var lancamento = _mapper.Map<Lancamento>(lancamentoDTO);
        await _lancamentoRepository.UpdateAsync(lancamento);
    }
}
