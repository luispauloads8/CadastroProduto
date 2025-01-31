﻿using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface IProdutoServicoService
{
    Task<IEnumerable<ProdutoServicoDTO>> GetProdutosServicos(); 
    Task<ProdutoServicoDTO> GetById(int? id);
    Task<List<ProdutoServicoDTO>> GetProdutoServicoTermo(string search);
    Task Add(ProdutoServicoDTO produtoServicoDTO);
    Task Update(ProdutoServicoDTO produtoDTO);
    Task Delete(int? id);
}
