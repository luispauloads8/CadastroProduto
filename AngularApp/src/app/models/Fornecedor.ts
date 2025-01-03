import { Cidade } from "./Cidade";

export interface Fornecedor{
    id?: number;
    descricao: string;
    CNPJ: string;
    telefone: string;
    endereco: string;
    CEP: string;
    observacao: string;
    cidadeId: number;
    email: string;
    cidade: Cidade;
}