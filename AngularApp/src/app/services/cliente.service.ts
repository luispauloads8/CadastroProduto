import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { Cliente } from '../models/Cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {


  ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) { }

  public GetCliente(): Observable<Cliente[]>{
    return this.http.get<Cliente[]>(`${this.ApiUrl}Clientes`);
  }

  public GetClienteId(id: number): Observable<Cliente>{
    return this.http.get<Cliente>(`${this.ApiUrl}Clientes/${id}`).pipe(take(1));
  }

  public GetClienteTermo(termo: string): Observable<Cliente[]>{
    return this.http.get<Cliente[]>(`${this.ApiUrl}Clientes/${termo}`).pipe(take(1));
  }

  public post(cliente: Cliente): Observable<Cliente>{
    return this.http.post<Cliente>(`${this.ApiUrl}Clientes`, cliente).pipe(take(1));
  }

  public put(cliente: Cliente): Observable<Cliente>{
    return this.http.put<Cliente>(`${this.ApiUrl}Clientes/${cliente.id}`, cliente).pipe(take(1));
  }

  public DeletarCliente(id: number): Observable<Cliente>{
    return this.http.delete<Cliente>(`${this.ApiUrl}Clientes/${id}`).pipe(take(1));
  }
  
}
