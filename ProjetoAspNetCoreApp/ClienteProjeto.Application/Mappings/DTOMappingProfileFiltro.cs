using AutoMapper;
using ClienteProjeto.Application.DTOs.Filtro;
using ClienteProjeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Mappings
{
    public class DTOMappingProfileFiltro : Profile
    {
        public DTOMappingProfileFiltro()
        {
            CreateMap<DtoFiltroLancamento, Lancamento>()
            .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom(src => src.EmpresaId))
            .ForMember(dest => dest.ContaContabilId, opt => opt.MapFrom(src => src.ContaContabilId))
            .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
            .ForMember(dest => dest.ProdutoServicoId, opt => opt.MapFrom(src => src.ProdutoServicoId))
            .ForMember(dest => dest.DataLancamento, opt => opt.MapFrom(src => src.VencimentoInicio)).ReverseMap(); // ou ajuste correto
                                                                                                     // Adicione os demais campos se necessário
        }
    }
}
