import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { Empresa } from '../models/Empresa';

@Injectable({
  providedIn: 'root'
})
export class EmpresaService {

  ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) { }

  public GetEmpresa(): Observable<Empresa[]>{
    return this.http.get<Empresa[]>(`${this.ApiUrl}empresas`);
  }

  public GetEmpresaId(id: number): Observable<Empresa>{
    return this.http.get<Empresa>(`${this.ApiUrl}Empresas/${id}`).pipe(take(1));
  }

  public GetEmpresaTermo(termo: string): Observable<Empresa[]>{
    return this.http.get<Empresa[]>(`${this.ApiUrl}Empresas/${termo}`).pipe(take(1));
  }

  public post(empresa: Empresa): Observable<Empresa>{
    return this.http.post<Empresa>(`${this.ApiUrl}Empresas`, empresa).pipe(take(1));
  }

  public put(empresa: Empresa): Observable<Empresa>{
    return this.http.put<Empresa>(`${this.ApiUrl}Empresas/${empresa.id}`, empresa).pipe(take(1));
  }

  public DeletarEmpresa(id: number): Observable<Empresa>{
    return this.http.delete<Empresa>(`${this.ApiUrl}Empresas/${id}`).pipe(take(1));
  }
}
