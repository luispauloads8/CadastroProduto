using ClienteProjeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.DTOs.Filtro
{
    public class DtoFiltroCategoria
    {
        public int Id { get; set; }

        public List<Categoria> Categoriasvm { get; set; }

        //public List<DtoCategoria> Categorias { get; set; }

        public ICollection<Categoria> ProdutoServicos { get; set; }
    }
}
