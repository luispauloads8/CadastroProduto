using ClienteProjeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Domain.Interfaces;

public interface IItensLancamentosRepository
{
    Task<IEnumerable<ItensLancamento>> GetLancamentoAsync();
    Task<ItensLancamento> GetByIdAsync(int? id);
    Task<ItensLancamento> CreateAsync(ItensLancamento Itenslancamento);
    Task<ItensLancamento> UpdateAsync(ItensLancamento Itenslancamento);
    Task<ItensLancamento> DeleteAsync(ItensLancamento Itenslancamento);
}
