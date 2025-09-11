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
    public class PessoaRepository : IPessoaRepository
    {
        private IDbContextFactory<ApplicationDbContext> _contextFactory;
        private ApplicationDbContext _pessoaContext;

        public PessoaRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext pessoaContext)
        {
            _contextFactory = contextFactory;
            _pessoaContext = pessoaContext;
        }

        public async Task<Pessoa> CreateAsync(Pessoa pessoa)
        {
            _pessoaContext.Add(pessoa);
            await _pessoaContext.SaveChangesAsync();
            return pessoa;
        }

        public async Task<Pessoa> DeleteAsync(Pessoa pessoa)
        {
            _pessoaContext.Remove(pessoa);
            await _pessoaContext.SaveChangesAsync();
            return pessoa;
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

        public async Task<Pessoa> GetByIdAsync(int? id)
        {
            return await _pessoaContext.Pessoas.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Pessoa>> GetPessoaAsync()
        {
            return await _pessoaContext.Pessoas.AsNoTracking().ToListAsync();
        }

        public Task<List<Pessoa>> GetPessoaTermoAsync(string search)
        {
            return _pessoaContext.Pessoas
            .Where(c => c.Nome.Contains(search))
            .ToListAsync();
        }

        public async Task<Pessoa> UpdateAsync(Pessoa pessoa)
        {
            var local = _pessoaContext.Set<Pessoa>().Local
                .FirstOrDefault(entry => entry.Id == pessoa.Id);

            if (local != null)
            {
                _pessoaContext.Entry(local).State = EntityState.Detached;
            }

            // Carrega a pessoa existente com o endereço
            var pessoaExistente = await _pessoaContext.Pessoas
                .Include(p => p.Endereco)
                .FirstOrDefaultAsync(p => p.Id == pessoa.Id);

            if (pessoaExistente == null)
                throw new Exception("Pessoa não encontrada.");

            // Atualiza os campos da pessoa
            pessoaExistente.Nome = pessoa.Nome;

            pessoaExistente.Telefone = pessoa.Telefone;
            pessoaExistente.CNPJ = pessoa.CNPJ;
            pessoaExistente.Email = pessoa.Email;

            // Atualiza os campos do endereço
            if (pessoaExistente.Endereco != null && pessoa.Endereco != null)
            {
                pessoaExistente.Endereco.Logradouro = pessoa.Endereco.Logradouro;
                pessoaExistente.Endereco.Cidade = pessoa.Endereco.Cidade;
                pessoaExistente.Endereco.CEP = pessoa.Endereco.CEP;
                pessoaExistente.Endereco.CaixaPostal = pessoa.Endereco.CaixaPostal;
            }

            await _pessoaContext.SaveChangesAsync();
            return pessoaExistente;

        }
    }
    
}
