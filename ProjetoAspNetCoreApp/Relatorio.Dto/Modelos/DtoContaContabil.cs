using Relatorio.Dto.Lancamento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.Modelos
{
    public class DtoContaContabil
    {
        public int Id { get; set; }

        public string Descricao { get; set; }
        public int GrupoContaId { get; set; }

        public DtoGrupoConta GrupoConta { get; set; }
        public ICollection<DtoLancamento> Lancamentos { get; set; }
    }
}
