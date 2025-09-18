using AutoMapper;
using ClienteProjeto.Application.DTOs.Filtro;
using Relatorio.Dto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Mappings
{
    public class DTOMappingProfilesViewModel : Profile
    {
        public DTOMappingProfilesViewModel()
        {
            CreateMap<ParametroEmissaoLancamentosVM, DtoFiltroLancamento>().ReverseMap();
            CreateMap<ParametroEmissaoCategoriaVM, DtoFiltroCategoria>().ReverseMap();
        }
    }
}
