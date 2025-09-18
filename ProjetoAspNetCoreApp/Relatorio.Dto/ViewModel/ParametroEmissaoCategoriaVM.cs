using Relatorio.Dto.Modelos;
using Relatorio.Dto.ViewModel.Categoria;
using Relatorio.Dto.ViewModel.ProdutoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Relatorio.Dto.ViewModel
{
    public class ParametroEmissaoCategoriaVM : ParametrosDeEmissaoBaseDR
    {

        public int Id { get; set; }
        
        [JsonPropertyName("categoriasvm")]
        public List<CategoriaVM> Categoriasvm { get; set; }

        public ICollection<ProdutoServiceVM> ProdutoServicos { get; set; }

        public List<DtoCategoria> Categorias { get; set; }
    }
}
