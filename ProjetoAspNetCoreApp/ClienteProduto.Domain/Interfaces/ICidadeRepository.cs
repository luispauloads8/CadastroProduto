﻿using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface ICidadeRepository
{
    Task<IEnumerable<Cidade>> GetCidadeAsync();
    Task<Cidade> GetByIdAsync(int? id);
    Task<List<Cidade>> GetCidadeTermoAsync(string termo);
    Task<Cidade> CreateAsync(Cidade cidade);
    Task<Cidade> UpdateAsync(Cidade cidade);
    Task<Cidade> DeleteAsync(Cidade cidade);
    Task EnsureConnectionOpenAsync();
}
