import { Cidade } from "./Cidade"


export interface Endereco{
    id?: number,
    logradouro: string,
    bairro: string
    caixaPostal: string
    cep: string,
    cidade: Cidade
}