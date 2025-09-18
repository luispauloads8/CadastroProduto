using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClienteProjeto.Infrastructure.Repositories;

public class LancamentoRepository : ILancamentoRepository
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ApplicationDbContext _lancamentosContext;

    public LancamentoRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext lancamentosContext)
    {
        _contextFactory = contextFactory;
        _lancamentosContext = lancamentosContext;
    }

    public async Task<Lancamento> CreateAsync(Lancamento lancamento)
    {
        _lancamentosContext.Add(lancamento);
       await _lancamentosContext.SaveChangesAsync();
       return lancamento;
    }

    public async Task<Lancamento> DeleteAsync(Lancamento lancamento)
    {
        _lancamentosContext.Remove(lancamento);
        await _lancamentosContext.SaveChangesAsync();
        return lancamento;
    }

    public async Task EnsureConnectionOpenAsync()
    {
        var context = _contextFactory.CreateDbContext();
        var connection = context.Database.GetDbConnection();
        Console.WriteLine("Estado atual da conexão: " + connection.State);
        if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
        {
            Console.WriteLine("Tentando abrir a conexão...");
            await connection.OpenAsync();
            Console.WriteLine("Conexão aberta.");
        }
        else if (connection.State == ConnectionState.Connecting)
        {
            Console.WriteLine("A conexão já está em processo de abertura.");
        }
    }

    public async Task<Lancamento?> GetByIdAsync(int? id)
    {
        return await _lancamentosContext.Lancamentos
            .Include(l => l.ContaContabil)
            .ThenInclude(c => c.GrupoConta)
            .FirstOrDefaultAsync(l => l.Id == id);
            //await _lancamentosContext.Lancamentos.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<IEnumerable<Lancamento>> GetLancamentoAsync()
    {
        return await _lancamentosContext.Lancamentos.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Lancamento>> GetLancamentoEmissaoAsync(int? empresaId, int? contaContabilId, DateTime? lancamentoInicio, DateTime? lancamentoFim)
    {
        var query = _lancamentosContext.Lancamentos
            .Include(l => l.Empresa)
            .Include(l => l.Cliente)
            .Include(l => l.ContaContabil)
            .ThenInclude(g => g.GrupoConta)
            .Include(p => p.ProdutoServico)
            .Include(i => i.ItensLancamentos)
            .AsQueryable();

        if (empresaId.HasValue)
            query = query.Where(l => l.EmpresaId == empresaId.Value);

        //if (contaContabilId.HasValue)
        //    query = query.Where(l => l.ContaContabilId == contaContabilId.Value);

        //if (clienteId.HasValue)
        //    query = query.Where(l => l.ClienteId == clienteId.Value);

        if (lancamentoInicio.HasValue)
        {
            query = query.Where(l => l.DataLancamento >= lancamentoInicio.Value.Date);
        }

        if (lancamentoFim.HasValue)
        {
            var fim = lancamentoFim.Value.Date.AddDays(1); // dia seguinte à meia-noite
            query = query.Where(l => l.DataLancamento < fim);
        }

        return await query.ToListAsync();
    }

    public async Task<Lancamento> UpdateAsync(Lancamento lancamento)
    {
        // Carregar o lançamento existente e seus itens relacionados
        var lancamentoExistente = await _lancamentosContext.Lancamentos
            .Include(l => l.ItensLancamentos)
            .FirstOrDefaultAsync(l => l.Id == lancamento.Id);

        if (lancamentoExistente == null)
        {
            throw new KeyNotFoundException("Lançamento não encontrado");
        }

        // Atualiza os campos do lançamento principal
        _lancamentosContext.Entry(lancamentoExistente).CurrentValues.SetValues(lancamento);

        // Atualiza ou adiciona os ItensLancamento
        foreach (var itemRecebido in lancamento.ItensLancamentos)
        {
            var itemExistente = lancamentoExistente.ItensLancamentos
                .FirstOrDefault(i => i.Id == itemRecebido.Id);

            if (itemExistente != null)
            {
                // Atualiza o item existente apenas se necessário
                _lancamentosContext.Entry(itemExistente).CurrentValues.SetValues(itemRecebido);
            }
            else
            {
                // Adiciona o novo item
                lancamentoExistente.ItensLancamentos.Add(itemRecebido);
            }
        }

        await _lancamentosContext.SaveChangesAsync();
        return lancamentoExistente;
    }

}
