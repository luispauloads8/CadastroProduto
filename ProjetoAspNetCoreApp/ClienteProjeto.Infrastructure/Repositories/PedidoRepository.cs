using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private IDbContextFactory<ApplicationDbContext> _contextFactory;
        private ApplicationDbContext _pedidoContext;

        public PedidoRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext pedidoContext)
        {
            _contextFactory = contextFactory;
            _pedidoContext = pedidoContext;
        }

        public async Task<Pedido> CreateAsync(Pedido pedido)
        {
            _pedidoContext.Add(pedido);
            await _pedidoContext.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> DeleteAsync(Pedido pedido)
        {
            _pedidoContext.Remove(pedido);
            await _pedidoContext.SaveChangesAsync();
            return pedido;
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

        public async Task<Pedido> GetByIdAsync(int? id)
        {
            return await _pedidoContext.Pedidos.Include(p => p.ItensPedido).ThenInclude(d => d.ProdutoServico).FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<PagedResult<Pedido>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _pedidoContext.Pedidos.AsQueryable();

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(c => c.Descricao)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Pedido>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<IEnumerable<Pedido>> GetPedidoAsync()
        {
            return await _pedidoContext.Pedidos.AsNoTracking().ToListAsync();
        }

        public Task<List<Pedido>> GetPedidoTermoAsync(string search)
        {
            throw new NotImplementedException();
        }

        public async Task<Pedido> UpdateAsync(Pedido pedido)
        {
            var pedidoAtual = await _pedidoContext.Pedidos
                .Include(p => p.ItensPedido)
                .FirstOrDefaultAsync(p => p.Id == pedido.Id);

            if (pedidoAtual == null)
                throw new Exception("Pedido não encontrado.");

            _pedidoContext.Entry(pedidoAtual).CurrentValues.SetValues(pedido);

            foreach (var itemDb in pedidoAtual.ItensPedido.ToList())
            {
                if (!pedido.ItensPedido.Any(i => i.Id == itemDb.Id))
                    _pedidoContext.Remove(itemDb);
            }

            foreach (var item in pedido.ItensPedido)
            {
                var itemExistente = pedidoAtual.ItensPedido
                    .FirstOrDefault(i => i.Id == item.Id);

                if (itemExistente == null)
                {
                    var novoItem = new ItensPedido
                    {
                        PedidoId = pedidoAtual.Id,
                        ProdutoServicoId = item.ProdutoServicoId,
                        Quantidade = item.Quantidade,
                        ValorItem = item.ValorItem,
                        ValorTotal = item.ValorTotal
                    };

                    pedidoAtual.ItensPedido.Add(novoItem);
                }
                else
                {
                    itemExistente.Quantidade = item.Quantidade;
                    itemExistente.ValorItem = item.ValorItem;
                    itemExistente.ValorTotal = item.ValorTotal;

                    itemExistente.ProdutoServicoId = item.ProdutoServicoId;
                }
            }

            await _pedidoContext.SaveChangesAsync();
            return pedidoAtual;
        }

    }
}
