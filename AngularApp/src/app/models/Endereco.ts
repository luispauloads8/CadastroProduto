import { Cidade } from "./Cidade"


export interface Endereco{
    Logradouro: string,
    Bairro: string
    CaixaPostal: string
    CEP: string,
    Cidade: Cidade
}