import { Categoria } from "./Categoria";
import { Lancamento } from "./Lancamento";

export interface ProdutoServico {
    id?: number;
    descricao: string;
    imagem: Blob | string;
    categoriaId: number,
    categoria: Categoria;
    lancamentos: Lancamento[];
    tipoMime : string;
}