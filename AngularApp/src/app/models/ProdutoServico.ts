import { Categoria } from "./Categoria";
import { Lancamento } from "./Lancamento";

export interface ProdutoServico {
    id?: number;
    descricao: string;
    imagem: Blob;
    categoria: Categoria;
    lancamentos: Lancamento[];
}