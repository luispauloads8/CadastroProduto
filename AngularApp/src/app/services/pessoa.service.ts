import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { Pessoa } from '../models/Pessoa';

@Injectable({
  providedIn: 'root'
})
export class PessoaService {


  ApiUrl = environment.UrlApi;
    
  constructor(private http: HttpClient) { }

  public GetPessoa(): Observable<Pessoa[]>{
    return this.http.get<Pessoa[]>(`${this.ApiUrl}Pessoas`);
  }

  public GetPessoaId(id: number): Observable<Pessoa>{
    return this.http.get<Pessoa>(`${this.ApiUrl}Pessoas/${id}`).pipe(take(1));
  }

  public GetPessoaTermo(termo: string): Observable<Pessoa[]>{
    return this.http.get<Pessoa[]>(`${this.ApiUrl}Pessoas/${termo}`).pipe(take(1));
  }

  public post(pessoa: Pessoa): Observable<Pessoa>{
    return this.http.post<Pessoa>(`${this.ApiUrl}Pessoas`, pessoa).pipe(take(1));
  }

  public put(pessoa: Pessoa): Observable<Pessoa>{
    return this.http.put<Pessoa>(`${this.ApiUrl}Pessoas/${pessoa.id}`, pessoa).pipe(take(1));
  }

  public DeletarPessoa(id: number): Observable<Pessoa>{
    return this.http.delete<Pessoa>(`${this.ApiUrl}Pessoas/${id}`).pipe(take(1));
  }
  
}
