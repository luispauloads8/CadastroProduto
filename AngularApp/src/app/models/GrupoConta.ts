import { ContaContabil } from "./ContaContabil";

export interface GrupoConta{
    id?: number;
    descricao: string;
    contaContabil: ContaContabil[];
}