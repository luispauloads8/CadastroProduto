import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Categoria } from '../models/Categoria';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

   ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) { }

  public GetCategorias(): Observable<Categoria[]> {
      return this.http.get<Categoria[]>(`${this.ApiUrl}Categorias`);
  };

  GetCategoriaId(id:Number) : Observable<Categoria> {
    return this.http.get<Categoria>(`${this.ApiUrl}Categorias/${id}`);
  }

  CriarCategoria(categoria: Categoria) : Observable<Categoria> {
    return this.http.post<Categoria>(`${this.ApiUrl}Categorias`,categoria);
  }

  EditarCategoria(id:Number, categoria: Categoria) : Observable<Categoria> {
    return this.http.put<Categoria>(`${this.ApiUrl}Categorias/${id}`, categoria);
  }

  DeletarCategoria(id:number) : Observable<Categoria> {
      return this.http.delete<Categoria>(`${this.ApiUrl}Categorias/${id}`);
  }

}
