import { Empresa } from "./Empresa";
import { Endereco } from "./Endereco";
import { EnumEstadoCivil } from "./EnumEstadoCivil";
import { EnumSexo } from "./EnumSexo";
import { EnumTipoPessoa } from "./EnumTipoPessoa";

export interface Pessoa{
    id?: number,
    email: string, 
    nome: string,
    telefone: string,
    cnpj: string,
    cpf: string,
    dataNascimento: Date,
    cnae: string,
    inscricaoEstadual: string,
    inscricaoMunicipal: string,
    estadoCivil: EnumEstadoCivil;
    nacionalidade: string;
    sexo: EnumSexo;
    tipoPessoa: EnumTipoPessoa,
    endereco: Endereco,
    empresa: Empresa,
    enderecoId: number
}
        
        