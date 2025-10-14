using ClienteProjeto.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDTO>> GetPedidos();
        Task<PedidoDTO> GetById(int? id);
        Task<List<PedidoDTO>> GetPedidoTermo(string search);
        Task Add(PedidoDTO pedidoDTO);
        Task Update(PedidoDTO pedidoDTO);
        Task Delete(int? id);
    }
}
