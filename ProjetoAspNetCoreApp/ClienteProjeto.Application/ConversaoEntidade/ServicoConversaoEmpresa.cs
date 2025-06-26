using ClienteProjeto.Application.DTOs;
using Relatorio.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.ConversaoEntidade
{
    public static class ServicoConversaoEmpresa
    {
        public static DtoEmpresa Conversao(EmpresaDTO empresa)
        {
            var dto = new DtoEmpresa 
            {
                Id = empresa.Id,
                NomeFantasia = empresa.NomeFantasia
            };

            return dto;
        }
    }
}
