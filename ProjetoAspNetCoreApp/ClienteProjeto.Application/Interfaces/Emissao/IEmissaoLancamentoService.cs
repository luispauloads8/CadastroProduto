using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.DTOs.Filtro;
using ClienteProjeto.Domain.Entities;
using Relatorio.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Interfaces.Emissao
{
    public interface IEmissaoLancamentoService
    {
        Task<IEnumerable<LancamentoDTO>> GetLancamentos(DtoFiltroLancamento dto);
    }
}
