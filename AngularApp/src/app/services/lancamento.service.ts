import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { Lancamento } from '../models/Lancamento';
import { table } from 'node:console';

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
    return this.http.get<Lancamento>(`${this.ApiUrl}Lancametos/${id}`).pipe(take(1));
  }

  public post(lancamento: Lancamento): Observable<Lancamento>{
    return this.http.post<Lancamento>(`${this.ApiUrl}Lancamentos`, lancamento).pipe(take(1));
  }

  public put(id: number, lancamento: Lancamento): Observable<Lancamento>{
    return this.http.put<Lancamento>(`${this.ApiUrl}Lancamentos/${id}`, lancamento).pipe(take(1));
  }

  public DeletarLancamento(id: number): Observable<Lancamento>{
    return this.http.delete<Lancamento>(`${this.ApiUrl}Lancamentos/${id}`).pipe(take(1));
  }

}
