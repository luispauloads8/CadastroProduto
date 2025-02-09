import { Cliente } from "./Cliente";
import { Empresa } from "./Empresa";
import { EnumEstado } from "./EnumEstado";
import { Fornecedor } from "./Fornecedor";

export interface Cidade{
    id?: number;
    descricao: string;
    estado: EnumEstado;
    estadoDescricao: string,
    clientesEndereco: Cliente;
    empresa: Empresa;
    fornecedor: Fornecedor;
}