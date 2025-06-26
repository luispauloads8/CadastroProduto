using ClienteProjeto.Application.DTOs.Filtro;
using ClienteProjeto.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Interfaces.Emissao
{
    public interface IEmissaoCategoriaService
    {
        Task<IEnumerable<CategoriaDTO>> GetCategorias(DtoFiltroCategoria dto);
    }
}
