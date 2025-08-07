import { Endereco } from "./Endereco";

export interface Pessoa{
    id: number,
    email: string, 
    nome: string,
    telefone: string,
    CNPJ: string,
    endereco: Endereco
}
        
        