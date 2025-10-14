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
            var local = _pedidoContext.Set<Pedido>().Local.FirstOrDefault(entry => entry.Id == pedido.Id);

            if (local != null)
            {
                _pedidoContext.Entry(local).State = EntityState.Detached;
            }
            _pedidoContext.Entry(pedido).State = EntityState.Modified;

            _pedidoContext.Update(pedido);
            await _pedidoContext.SaveChangesAsync();
            return pedido;
        }
    }
}
