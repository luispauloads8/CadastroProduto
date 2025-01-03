import { Lancamento } from "./Lancamento";

export interface ItensLancamentos{
    id?: number;
    quantidade: number;
    valorItem: number;
    lancamentoId: number;
    lancamento: Lancamento;
}