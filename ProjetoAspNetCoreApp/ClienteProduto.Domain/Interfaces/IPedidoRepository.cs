using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetPedidoAsync();
        Task<Pedido> GetByIdAsync(int? id);
        Task<List<Pedido>> GetPedidoTermoAsync(string search);
        Task<Pedido> CreateAsync(Pedido pedido);
        Task<Pedido> UpdateAsync(Pedido pedido);
        Task<Pedido> DeleteAsync(Pedido pedido);
        Task<PagedResult<Pedido>> GetPagedAsync(int pageNumber, int pageSize);
        Task EnsureConnectionOpenAsync();
    }
}
