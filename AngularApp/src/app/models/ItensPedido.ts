import { ProdutoServico } from "./ProdutoServico";

export interface ItensPedido {
    id: number,
    nome: string,
    valorItem: number,
    valorTotal: number,
    quantidade: number,
    produtoServico: ProdutoServico
}