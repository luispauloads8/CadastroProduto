import { Cliente } from "./Cliente";
import { ContaContabil } from "./ContaContabil";
import { Empresa } from "./Empresa";
import { ItensLancamentos } from "./ItensLancamentos";
import { ProdutoServico } from "./ProdutoServico";

export interface Lancamento {
    id?: number;
    empresaId: number;
    contaContabilId: number;
    produtoServicoId: number;
    clienteId: number; 
    observacao: string; 
    dataLancamento?: Date;
    valor: number;
    contaContabil: ContaContabil; 
    empresa: Empresa;
    cliente: Cliente; 
    produtoServico: ProdutoServico; 
    itensLancamento: ItensLancamentos[]; 
}