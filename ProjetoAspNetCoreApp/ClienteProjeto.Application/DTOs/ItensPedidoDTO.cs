using ClienteProjeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.DTOs
{
    public class ItensPedidoDTO
    {
        [Required(ErrorMessage = "Informe a quantidade")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Informe o valor")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal ValorItem { get; set; }

        [Required(ErrorMessage = "Informe o valor")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal ValorTotal { get; set; }

        public Pedido Pedido { get; set; }
    }
}
