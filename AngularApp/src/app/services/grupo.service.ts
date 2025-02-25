import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { GrupoConta } from '../models/GrupoConta';

@Injectable({
  providedIn: 'root'
})
export class GrupoService {

  apiUrl = environment.UrlApi;

  constructor(private http: HttpClient) { }

  public GetGrupoConta(): Observable<GrupoConta[]>{
    return this.http.get<GrupoConta[]>(`${this.apiUrl}GrupoContas`);
  }

  public GetGrupoContaId(id: number): Observable<GrupoConta>{
    return this.http.get<GrupoConta>(`${this.apiUrl}GrupoContas/${id}`).pipe(take(1));
  }

  public GetGrupoContaTermo(termo: string): Observable<GrupoConta[]>{
    return this.http.get<GrupoConta[]>(`${this.apiUrl}GrupoContas/${termo}`).pipe(take(1));
  }

  public post(grupoConta: GrupoConta): Observable<GrupoConta>{
    return this.http.post<GrupoConta>(`${this.apiUrl}GrupoContas`, grupoConta).pipe(take(1));
  }

  public put(grupoConta: GrupoConta): Observable<GrupoConta>{
    return this.http.put<GrupoConta>(`${this.apiUrl}GrupoContas/${grupoConta.id}`, grupoConta).pipe(take(1));
  }

  public DeletarGrupoConta(id: number): Observable<GrupoConta>{
    return this.http.delete<GrupoConta>(`${this.apiUrl}GrupoContas/${id}`).pipe(take(1));
  }
}
