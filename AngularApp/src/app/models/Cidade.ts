import { Cliente } from "./Cliente";
import { Empresa } from "./Empresa";
import { Fornecedor } from "./Fornecedor";

export interface Cidade{
    id?: number;
    descricao: string;
    clientesEndereco: Cliente;
    empresa: Empresa;
    fornecedor: Fornecedor;
}