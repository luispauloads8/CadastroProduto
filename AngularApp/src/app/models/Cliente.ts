import { Cidade } from "./Cidade";
import { EnumEstadoCivil } from "./EnumEstadoCivil";
import { EnumSexo } from "./EnumSexo";
import { Lancamento } from "./Lancamento";
import { Pessoa } from "./Pessoa";

export interface Cliente{
     id?: number;
     nome: string;
     CPF: string; 
     dataNascimento: Date;
     RG: string;
     sexo: EnumSexo;
     CEP: string;
     endereco: string;
     telefone: string;
     email: string;
     estadoCivil: EnumEstadoCivil;
     nacionalidade: string;
     observacao: string;
     cidadeEnderecoId: number;
     cidade: Cidade;
     lancamento: Lancamento[];
     pessoaId: Pessoa;
     pessoa: Pessoa;
}