import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { Categoria } from '../models/Categoria';
import { EmissaoCategoria } from '../models/emissao/categoria/EmissaoCategoria';
import { ParametroEmissaoCategoriaVM } from '../models/emissao/categoria/ParametroEmissaoCategoriaVM';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

   ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) { }

  public GetCategorias(): Observable<Categoria[]> {
      return this.http.get<Categoria[]>(`${this.ApiUrl}Categorias`).pipe(take(1));
  };

  public GetCategoriaId(id:Number) : Observable<Categoria> {
    return this.http.get<Categoria>(`${this.ApiUrl}Categorias/${id}`).pipe(take(1));
  }

  public GetCategoriaTermo(termo:string): Observable<Categoria[]>{
    return this.http.get<Categoria[]>(`${this.ApiUrl}Categorias/${termo}`).pipe(take(1));
  }

  public post(categoria: Categoria) : Observable<Categoria> {
    return this.http.post<Categoria>(`${this.ApiUrl}Categorias`,categoria).pipe(take(1));
  }

  public put(categoria: Categoria) : Observable<Categoria> {
    return this.http.put<Categoria>(`${this.ApiUrl}Categorias/${categoria.id}`, categoria).pipe(take(1));
  }

  DeletarCategoria(id:number) : Observable<Categoria> {
      return this.http.delete<Categoria>(`${this.ApiUrl}Categorias/${id}`).pipe(take(1));
  }

  public imprimirLancamento(categoria: ParametroEmissaoCategoriaVM): Observable<Blob> {
    return this.http.post<Blob>(`${this.ApiUrl}EmissaoCategoria/emissao_categoria`, categoria, {
      responseType: 'blob' as 'json'
    });
  }

}
