using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Relatorio.Dto.Modelos
{
    public class DtoCategoria
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        public string Descricao { get; set; }

        public ICollection<DtoProdutoServico> ProdutoServicos { get; set; }
    }
}
