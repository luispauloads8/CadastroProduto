using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.Modelos
{
    public class DtoGrupoConta
    {
        public int Id { get; set; }

        public string Descricao { get; set; }
        public ICollection<DtoContaContabil> ContaContabeis { get; set; }
    }
}
