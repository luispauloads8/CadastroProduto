using ClienteProjeto.Application.DTOs;
using Relatorio.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.ConversaoEntidade
{
    public static class ServicoConversaoCategoria
    {
        public static List<DtoCategoria> Conversao(IEnumerable<CategoriaDTO> categorias)
        {
            var listCategoria = new List<DtoCategoria>();

            foreach (var categoria in categorias)
            {
                var listcategoria = new DtoCategoria
                {
                    Id = categoria.Id,
                    Descricao = categoria.Descricao
                };
                listCategoria.Add(listcategoria);
            }
            return listCategoria;
        }
    }
}
