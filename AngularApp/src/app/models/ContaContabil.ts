import { Empresa } from "./Empresa";
import { GrupoConta } from "./GrupoConta";
import { Lancamento } from "./Lancamento";

export interface ContaContabil{
     id?: number;
     descricao: string;
     grupoContaId: number;
     grupoConta: GrupoConta;
     empresa: Empresa;
     empresaId: number;
     lancamentos: Lancamento[];
}