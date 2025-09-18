import { Endereco } from "./Endereco";

export interface Pessoa{
    id: number,
    email: string, 
    nome: string,
    telefone: string,
    cnpj: string,
    endereco: Endereco,
    enderecoId: number
}
        
        