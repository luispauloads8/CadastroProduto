using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private IDbContextFactory<ApplicationDbContext> _contextFactory;
        private ApplicationDbContext _enderecoContext;

        public EnderecoRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext enderecoContext)
        {
            _contextFactory = contextFactory;
            _enderecoContext = enderecoContext;
        }

        public Task<Endereco> CreateAsync(Endereco endereco)
        {
            throw new NotImplementedException();
        }

        public Task<Endereco> DeleteAsync(Endereco endereco)
        {
            throw new NotImplementedException();
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

        public async Task<Endereco> GetByIdAsync(int? id)
        {
            return await _enderecoContext.Enderecos.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<IEnumerable<Endereco>> GetEnderecoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Endereco>> GetEnderecoTermoAsync(string search)
        {
            throw new NotImplementedException();
        }

        public Task<Endereco> UpdateAsync(Endereco endereco)
        {
            throw new NotImplementedException();
        }
    }
}
