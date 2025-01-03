import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
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



  DeletarProdutoServico(id: number) :Observable<ProdutoServico> { 
    return this.http.delete<ProdutoServico>(`${this.ApiUrl}Produtos/${id}`);

  }
}
