import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Pedido } from '../models/Pedido';
import { Observable, take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {

  ApiUrl = environment.UrlApi;
  
  constructor(private http: HttpClient) { }

    public GetPedido(): Observable<Pedido[]>{
      return this.http.get<Pedido[]>(`${this.ApiUrl}Pedidos`);
    }

    public GetPedidoId(id: number): Observable<Pedido>{
      return this.http.get<Pedido>(`${this.ApiUrl}Pedidos/${id}`).pipe(take(1));
    }

    public post(pedido: Pedido): Observable<Pedido>{
      return this.http.post<Pedido>(`${this.ApiUrl}Pedidos`, pedido).pipe(take(1));
    }

    public put(pedido: Pedido): Observable<Pedido>{
      return this.http.put<Pedido>(`${this.ApiUrl}Pedidos/${pedido.id}`, pedido).pipe(take(1));
    }

    public DeletarPedido(id: number): Observable<Pedido>{
      return this.http.delete<Pedido>(`${this.ApiUrl}Pedidos/${id}`).pipe(take(1));
    }
}
