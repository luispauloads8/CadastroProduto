import { Cliente } from "./Cliente";
import { EnumFormaDePagamento } from "./EnumFormaDePagamento";
import { EnumStatusPedido } from "./EnumStatusPedido";
import { ItensPedido } from "./ItensPedido";
import { ProdutoServico } from "./ProdutoServico";

export interface Pedido {
    id: number,
    descricao: string,
    cliente: Cliente, 
    clienteId: number;
    statusPedido: EnumStatusPedido,
    formaDePagamento: EnumFormaDePagamento,
    itensPedido: ItensPedido[], 
    valorTotal: number, 
    dataPedido: Date | string,
    telefone: string,
}