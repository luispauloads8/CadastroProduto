using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Infrastructure.Repositories;

public class ItensLancamentoRepository : IItensLancamentosRepository
{

    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ApplicationDbContext _itenslancamentosContext;

    public ItensLancamentoRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext lancamentosContext)
    {
        _contextFactory = contextFactory;
        _itenslancamentosContext = lancamentosContext;
    }

    public Task<ItensLancamento> CreateAsync(ItensLancamento Itenslancamento)
    {
        throw new NotImplementedException();
    }

    public Task<ItensLancamento> DeleteAsync(ItensLancamento Itenslancamento)
    {
        throw new NotImplementedException();
    }

    public async Task<ItensLancamento> GetByIdAsync(int? id)
    {
        return await _itenslancamentosContext.ItensLancamentos.AsNoTracking().FirstOrDefaultAsync(i => i.LancamentoId == id);
    }

    public Task<IEnumerable<ItensLancamento>> GetLancamentoAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ItensLancamento> UpdateAsync(ItensLancamento Itenslancamento)
    {
        throw new NotImplementedException();
    }
}
