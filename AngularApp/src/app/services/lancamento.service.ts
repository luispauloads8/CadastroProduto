import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Lancamento } from '../models/Lancamento';

@Injectable({
  providedIn: 'root'
})
export class LancamentoService {

    ApiUrl = environment.UrlApi;
  
    constructor(private http: HttpClient) { }

    public GetLancamentos() : Observable<Lancamento[]>{
      return this.http.get<Lancamento[]>(`${this.ApiUrl}Lancamentos`);
    }
}
