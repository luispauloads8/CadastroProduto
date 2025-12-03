using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.DTOs
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
