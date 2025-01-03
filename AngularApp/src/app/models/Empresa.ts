import { Cidade } from "./Cidade";
import { Lancamento } from "./Lancamento";
import { Usuario } from "./Usuario";

export interface Empresa{
 id?: number;
 razaoSocial: string;
 nomeFantasia: string;
 CNPJ: string;
 email: string;
 telefone: string;
 cidadeEmpresaId: number;
 cidade: Cidade;
 usuarios: Usuario[];
 lancamentos: Lancamento[];
}