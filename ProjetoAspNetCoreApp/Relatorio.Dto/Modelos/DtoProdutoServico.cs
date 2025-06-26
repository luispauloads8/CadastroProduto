using Relatorio.Dto.Lancamento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.Modelos
{
    public class DtoProdutoServico
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public byte[] ImagemByteArray { get; set; }
        public int CategoriaId { get; set; }
        public DtoCategoria Categoria { get; set; }

        public ICollection<DtoLancamento> Lancamentos { get; set; }
    }
}
