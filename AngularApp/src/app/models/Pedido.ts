import { Cliente } from "./Cliente";
import { EnumStatusPedido } from "./EnumStatusPedido";
import { ProdutoServico } from "./ProdutoServico";

export interface Pedido {
    id: number,
    descricao: string,
    quantidade: number,
    produto: ProdutoServico,
    cliente: Cliente, 
    statusPedido: EnumStatusPedido
}