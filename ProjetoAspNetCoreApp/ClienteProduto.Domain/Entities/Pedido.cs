using ClienteProjeto.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Domain.Entities
{
    public class Pedido : Entity
    {
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public string Telefone { get; set; }
        public DateTime DataPedido { get; set; }
        public FormaDePagamento FormaDePagamento { get; set; }
        public StatusPedido Status { get; set; }

        public Cliente Cliente { get; set; }
        public ICollection<ItensPedido> ItensPedido { get; set; }

        #region "EF"
        public int ClienteId { get; set; }      
        public int ItensPedidoId { get; set; }

        #endregion
    }
}
