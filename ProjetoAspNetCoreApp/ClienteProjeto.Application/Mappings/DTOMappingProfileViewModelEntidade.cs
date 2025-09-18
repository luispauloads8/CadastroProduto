using AutoMapper;
using ClienteProjeto.Domain.Entities;
using Relatorio.Dto.ViewModel.Categoria;
using Relatorio.Dto.ViewModel.Lancamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Mappings
{
    public class DTOMappingProfileViewModelEntidade : Profile
    {
        public DTOMappingProfileViewModelEntidade()
        {
            CreateMap<LancamentoVM, Lancamento>();
            CreateMap<CategoriaVM, Categoria>().ReverseMap();
        }
    }
}
