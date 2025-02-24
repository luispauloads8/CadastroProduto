import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { ContaContabil } from '../models/ContaContabil';

@Injectable({
  providedIn: 'root'
})
export class ContaService {

  ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) { }

  public GetContaContabil(): Observable<ContaContabil[]>{
    return this.http.get<ContaContabil[]>(`${this.ApiUrl}ContaContabeis`);
  }

  public GetContaContabilId(id: number): Observable<ContaContabil>{
    return this.http.get<ContaContabil>(`${this.ApiUrl}ContaContabeis/${id}`).pipe(take(1));
  }

  public GetContaContabilTermo(termo: string): Observable<ContaContabil[]>{
    return this.http.get<ContaContabil[]>(`${this.ApiUrl}ContaContabeis/${termo}`).pipe(take(1));
  }

  public post(contaContabil: ContaContabil): Observable<ContaContabil>{
    return this.http.post<ContaContabil>(`${this.ApiUrl}ContaContabeis`, contaContabil).pipe(take(1));
  }

  public put(contaContabil: ContaContabil): Observable<ContaContabil>{
    return this.http.put<ContaContabil>(`${this.ApiUrl}ContaContabeis/${contaContabil.id}`, contaContabil).pipe(take(1));
  }

  public DeletarContaContabil(id: number): Observable<ContaContabil>{
    return this.http.delete<ContaContabil>(`${this.ApiUrl}ContaContabeis/${id}`).pipe(take(1));
  }
}
