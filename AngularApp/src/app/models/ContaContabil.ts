import { GrupoConta } from "./GrupoConta";
import { Lancamento } from "./Lancamento";

export interface ContaContabil{
     id?: number;
     descricao: string;
     grupoContaId: number;
     grupoConta: GrupoConta;
     lancamentos: Lancamento[];
}