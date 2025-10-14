using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Enum;
using ClienteProjeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private IPedidoRepository _pedidoRepository;
        private IClienteRepository _clienteRepository;
        private IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository pedidoRepository, IMapper mapper, IClienteRepository clienteRepository, IPessoaRepository pessoaRepository)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _pessoaRepository = pessoaRepository;
        }

        public async Task Add(PedidoDTO pedidoDTO)
        {
            await _pedidoRepository.EnsureConnectionOpenAsync();
            var pedidoEntity = _mapper.Map<Pedido>(pedidoDTO);
            pedidoEntity.Cliente = null;

            await _pedidoRepository.CreateAsync(pedidoEntity);
        }


        public async Task Delete(int? id)
        {
            await _pedidoRepository.EnsureConnectionOpenAsync();
            var pedidoEntity = await _pedidoRepository.GetByIdAsync(id);
            await _pedidoRepository.DeleteAsync(pedidoEntity);

        }

        public async Task<PedidoDTO> GetById(int? id)
        {
            await _pedidoRepository.EnsureConnectionOpenAsync();
            var pedidoEntity = await _pedidoRepository.GetByIdAsync(id);

            var clienteEntity = await _clienteRepository.GetByIdAsync(pedidoEntity.ClienteId);
            pedidoEntity.Cliente = clienteEntity;

            var pessoaRepository = await _pessoaRepository.GetByIdAsync(clienteEntity.PessoaId);
            pedidoEntity.Cliente.Pessoa = pessoaRepository;

            return _mapper.Map<PedidoDTO>(pedidoEntity);
        }

        public async Task<IEnumerable<PedidoDTO>> GetPedidos()
        {
            await _pedidoRepository.EnsureConnectionOpenAsync();
            var pedidoEntity = await _pedidoRepository.GetPedidoAsync();
            return _mapper.Map<IEnumerable<PedidoDTO>>(pedidoEntity);
        }

        public Task<List<PedidoDTO>> GetPedidoTermo(string search)
        {
            throw new NotImplementedException();
        }

        public async Task Update(PedidoDTO pedidoDTO)
        {
            await _pedidoRepository.EnsureConnectionOpenAsync();
            var pedidoEntity = _mapper.Map<Pedido>(pedidoDTO);

            await _pedidoRepository.UpdateAsync(pedidoEntity);
        }
    }
}
