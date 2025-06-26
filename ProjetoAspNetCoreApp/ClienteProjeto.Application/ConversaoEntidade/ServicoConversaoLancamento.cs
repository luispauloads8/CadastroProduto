using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Domain.Entities;
using Relatorio.Dto.Lancamento;
using Relatorio.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.ConversaoEntidade
{
    public static class ServicoConversaoLancamento
    {
        public static List<DtoLancamento> Converta(IEnumerable<LancamentoDTO> lancamentos)
        {
            var listLancamento = new List<DtoLancamento>();

            foreach (var dto in lancamentos)
            {
                var dtoLancamento = new DtoLancamento()
                {
                    Id = dto.Id,
                    Observacao = dto.Observacao,
                    DataLancamento = dto.DataLancamento,
                    Valor = dto.Valor,
                    EmpresaId = dto.EmpresaId,
                    ContaContabilId = dto.ContaContabilId,
                    ProdutoServicoId = dto.ProdutoServicoId,
                    ClienteId = dto.ClienteId,
                    ContaContabil = ServicoConversaoContaContabil.Conversao(dto.ContaContabil),
                    Empresa = ServicoConversaoEmpresa.Conversao(dto.Empresa),
                    ProdutoServico = ServicoConversaoProdutoServico.Converta(dto.ProdutoServico),
                    ItensLancamentos = ServicoConversaoItensLancamento.Conversao(dto.ItensLancamentos)

                };
                listLancamento.Add(dtoLancamento);
            }

           
            return listLancamento;
        }
    }
}
