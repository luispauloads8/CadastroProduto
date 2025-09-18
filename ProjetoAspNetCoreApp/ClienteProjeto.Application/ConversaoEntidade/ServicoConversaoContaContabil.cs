using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Domain.Entities;
using Relatorio.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.ConversaoEntidade
{
    public static class ServicoConversaoContaContabil
    {
        public static DtoContaContabil Conversao(ContaContabilDTO contaContabil)
        {
            var dto = new DtoContaContabil
            {
                Id = contaContabil.Id,
                Descricao = contaContabil.Descricao,
                GrupoConta = ServicoConversaoGrupoConta.Converta(contaContabil.GrupoConta)
            };

            return dto;
        }
    }
}
