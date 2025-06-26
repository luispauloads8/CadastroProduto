using Relatorio.Dto.Lancamento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.Modelos
{
    public class DtoItensLancamento
    {
        public int Id { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorItem { get; set; }
        public int LancamentoId { get; set; }
        public DtoLancamento Lancamento { get; set; }
    }
}
