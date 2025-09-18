using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.Modelos
{
    public class DtoLancamento
    {
        public int Id {  get; set; }

        public string Observacao { get; set; }

        public DateTime DataLancamento { get; set; }

        public decimal Valor { get; set; }

        public int EmpresaId { get; set; }
        public int ContaContabilId { get; set; }
        public int ProdutoServicoId { get; set; }
        public int ClienteId { get; set; }

        public DtoContaContabil ContaContabil { get; set; }
        public DtoEmpresa Empresa { get; set; }
        public DtoCliente Cliente { get; set; }
        public DtoProdutoServico ProdutoServico { get; set; }
        public ICollection<DtoItensLancamento> ItensLancamentos { get; set; }
    }
}
