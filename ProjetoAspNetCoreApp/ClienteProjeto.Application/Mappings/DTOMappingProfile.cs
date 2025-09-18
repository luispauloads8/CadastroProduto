using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Application.Mappings;

public class DTOMappingProfile : Profile
{
    public DTOMappingProfile()
    {
        CreateMap<ProdutoServico, ProdutoServicoDTO>().ReverseMap().ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.ImagemByteArray));
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Cidade, CidadeDTO>().ReverseMap();
        CreateMap<Cliente, ClienteDTO>().ReverseMap();
        CreateMap<Pessoa, PessoaDTO>().ReverseMap();
        CreateMap<ContaContabil, ContaContabilDTO>().ReverseMap();
        CreateMap<Empresa, EmpresaDTO>().ReverseMap();
        CreateMap<Fornecedor, FornecedorDTO>().ReverseMap();
        CreateMap<Endereco, EnderecoDTO>().ReverseMap();
        CreateMap<GrupoConta, GrupoContaDTO>().ReverseMap();
        CreateMap<ItensLancamento, ItensLancamentoDTO>().ReverseMap();
        CreateMap<Lancamento, LancamentoDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();
    }
}
