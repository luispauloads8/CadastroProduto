using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Domain.Entities
{
    public class ItensPedido : Entity
    {
        public int Quantidade { get; set; }
        public decimal ValorItem { get; set; }
        public decimal ValorTotal { get; set; }
        public Pedido Pedido { get; set; }
        public ProdutoServico ProdutoServico { get; set; }

        #region "EF"
        public int PedidoId { get; set; }
        public int ProdutoServicoId { get; set; }

        #endregion

    }
}
