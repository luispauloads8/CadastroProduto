using Relatorio.Dto.Lancamento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.Modelos
{
    public class DtoEmpresa
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }


        public string CNPJ { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public int CidadeId { get; set; }

        public DtoCidade Cidade { get; set; }
        public ICollection<DtoUsuario> Usuarios { get; set; }
        public ICollection<DtoLancamento> Lancamentos { get; set; }
    }
}
