using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Mappings;

public class DTOMappingProfile : Profile
{
    public DTOMappingProfile()
    {
        CreateMap<ProdutoServico, ProdutoServicoDTO>().ReverseMap();
    }
}
