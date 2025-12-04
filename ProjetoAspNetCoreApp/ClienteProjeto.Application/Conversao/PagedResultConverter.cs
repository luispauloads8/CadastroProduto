using AutoMapper;
using ClienteProjeto.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Conversao
{
    public class PagedResultConverter<TSource, TDestination>
     : ITypeConverter<PagedResult<TSource>, PagedResult<TDestination>>
    {
        private readonly IMapper _mapper;

        public PagedResultConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PagedResult<TDestination> Convert(PagedResult<TSource> source, PagedResult<TDestination> destination, ResolutionContext context)
        {
            return new PagedResult<TDestination>
            {
                Items = _mapper.Map<IEnumerable<TDestination>>(source.Items),
                TotalItems = source.TotalItems,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize
            };
        }
    }

}
