import { Empresa } from "./Empresa";

export interface Usuario{
    id?: number;
    nome: string;
    email: string;
    password: string;
    empresaId: number;
    empresa: Empresa;
}