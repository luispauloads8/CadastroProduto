using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.DTOs
{
    public class PedidoDTO
    {
        [Required(ErrorMessage = "Informe descrição do pedido")]
        [MinLength(3)]
        [MaxLength(300)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a quantidade")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Informe o valor")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public double ValorTotal { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatorio")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Data Pedido é obrigatorio")]
        public DateTime DataPedido { get; set; }

        public FormaDePagamento FormaDePagamento { get; set; }
        public StatusPedido Status { get; set; }

        public Cliente Cliente { get; set; }
        public ProdutoServico ProdutoServico { get; set; }
    }
}
