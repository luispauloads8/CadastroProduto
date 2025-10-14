import { Cliente } from "./Cliente";
import { EnumFormaDePagamento } from "./EnumFormaDePagamento";
import { EnumStatusPedido } from "./EnumStatusPedido";
import { ItensPedido } from "./ItensPedido";
import { ProdutoServico } from "./ProdutoServico";

export interface Pedido {
    id: number,
    descricao: string,
    produto: ProdutoServico,
    cliente: Cliente, 
    statusPedido: EnumStatusPedido,
    formaDePagamento: EnumFormaDePagamento,
    itensPedido: ItensPedido[], 
    valorTotal: number

}