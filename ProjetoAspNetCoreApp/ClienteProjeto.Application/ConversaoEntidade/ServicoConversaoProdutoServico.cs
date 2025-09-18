using ClienteProjeto.Application.DTOs;
using Relatorio.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.ConversaoEntidade
{
    public static class ServicoConversaoProdutoServico
    {
        public static DtoProdutoServico Converta(ProdutoServicoDTO produtoServicoDTO)
        {
            var dto = new DtoProdutoServico
            { 
                Id = produtoServicoDTO.Id,
                Descricao = produtoServicoDTO.Descricao
            };

            return dto;
        }
    }
}
