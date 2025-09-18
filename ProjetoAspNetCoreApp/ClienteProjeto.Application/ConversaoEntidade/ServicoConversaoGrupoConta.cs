using ClienteProjeto.Application.DTOs;
using Relatorio.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.ConversaoEntidade
{
    public static class ServicoConversaoGrupoConta
    {
        public static DtoGrupoConta Converta(GrupoContaDTO grupoContaDTO)
        {
            var dto = new DtoGrupoConta()
            {
                Id = grupoContaDTO.Id,
                Descricao = grupoContaDTO.Descricao
            };

            return dto;
        }
    }
}
