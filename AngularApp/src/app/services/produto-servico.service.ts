import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { ProdutoServico } from '../models/ProdutoServico';

@Injectable({
  providedIn: 'root'
})
export class ProdutoServicoService {

  ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) { }

  public GetProdutoServico() :Observable<ProdutoServico[]> {
    return this.http.get<ProdutoServico[]>(`${this.ApiUrl}Produtos`);
  }

  public GetProdutoServicoId(id: number) :Observable<ProdutoServico>{
    return this.http.get<ProdutoServico>(`${this.ApiUrl}Produtos/${id}`).pipe(take(1));
  }

    public post(produtoServico: ProdutoServico) : Observable<ProdutoServico> {
      return this.http.post<ProdutoServico>(`${this.ApiUrl}Produtos`,produtoServico).pipe(take(1));
    }

  public put(id: number, produtoServico: ProdutoServico): Observable<ProdutoServico>{
    return this.http.put<ProdutoServico>(`${this.ApiUrl}Produtos/${id}`, produtoServico).pipe(take(1));
  }

  DeletarProdutoServico(id: number) :Observable<ProdutoServico> { 
    return this.http.delete<ProdutoServico>(`${this.ApiUrl}Produtos/${id}`).pipe(take(1));
  }
}
