import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { Lancamento } from '../models/Lancamento';
import { table } from 'node:console';
import { EmissaoLancamento } from '../models/emissao/lancamento/EmissaoLancamento';

@Injectable({
  providedIn: 'root'
})
export class LancamentoService {

  ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) { }

  public GetLancamentos() : Observable<Lancamento[]>{
    return this.http.get<Lancamento[]>(`${this.ApiUrl}Lancamentos`);
  }

  public GetLancamentoId(id: number): Observable<Lancamento>{
    return this.http.get<Lancamento>(`${this.ApiUrl}Lancamentos/${id}`).pipe(take(1));
  }

  public post(lancamento: Lancamento): Observable<Lancamento>{
    return this.http.post<Lancamento>(`${this.ApiUrl}Lancamentos`, lancamento).pipe(take(1));
  }

  public put(lancamento: Lancamento): Observable<Lancamento>{
    return this.http.put<Lancamento>(`${this.ApiUrl}Lancamentos/${lancamento.id}`, lancamento).pipe(take(1));
  }

  public DeletarLancamento(id: number): Observable<Lancamento>{
    return this.http.delete<Lancamento>(`${this.ApiUrl}Lancamentos/${id}`).pipe(take(1));
  }

  public imprimirLancamento(lancamento: EmissaoLancamento): Observable<Blob> {
    return this.http.post<Blob>(`${this.ApiUrl}Relatorios/emissao_lancamento`, lancamento, {
      responseType: 'blob' as 'json'
    });
  }

}
