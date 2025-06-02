import { Empresa } from "./Empresa";

export interface Usuario{
    nome: string;
    email: string;
    password: string;
    empresaId: number;
    empresa: Empresa;
}