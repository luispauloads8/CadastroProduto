using ClienteProjeto.Application.DTOs;
using Relatorio.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.ConversaoEntidade
{
    public static class ServicoConversaoItensLancamento
    {
        public static ICollection<DtoItensLancamento> Conversao(ICollection<ItensLancamentoDTO> itensLancamentoDTO)
        {
            if (itensLancamentoDTO == null) return new List<DtoItensLancamento>();

            var listaDto = itensLancamentoDTO.Select(item => new DtoItensLancamento
            {
                Id = item.Id,
                Quantidade = item.Quantidade,
                ValorItem = item.ValorItem
                // Adicione outros campos se necessário
            }).ToList();

            return listaDto;
        }
    }
}
