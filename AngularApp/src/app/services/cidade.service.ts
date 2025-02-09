import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { Cidade } from '../models/Cidade';

@Injectable({
  providedIn: 'root'
})
export class CidadeService {

  ApiUrl = environment.UrlApi;

  constructor(private htpp: HttpClient) { }

  public GetCidade(): Observable<Cidade[]>{
    return this.htpp.get<Cidade[]>(`${this.ApiUrl}Cidades`);
  }

  public GetCidadeId(id: number): Observable<Cidade>{
    return this.htpp.get<Cidade>(`${this.ApiUrl}Cidades/${id}`).pipe(take(1));
  }

  public GetCidadeTermo(termo: string): Observable<Cidade[]>{
    return this.htpp.get<Cidade[]>(`${this.ApiUrl}Cidades/${termo}`).pipe(take(1));
  }

  public post(cidade: Cidade): Observable<Cidade>{
    return this.htpp.post<Cidade>(`${this.ApiUrl}Cidades`, cidade).pipe(take(1));
  }

  public put(cidade: Cidade): Observable<Cidade>{
    return this.htpp.put<Cidade>(`${this.ApiUrl}Cidades/${cidade.id}`, cidade).pipe(take(1));
  }

  public DeletarCidade(id: number): Observable<Cidade>{
    return this.htpp.delete<Cidade>(`${this.ApiUrl}Cidades/${id}`).pipe(take(1));
  }
  
}
