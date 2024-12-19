import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Categoria } from '../models/Categoria';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

   ApiUrl = environment.UrlApi;

  constructor(private http: HttpClient) { }

  public GetCategoria(): Observable<Categoria> {
      return this.http.get<Categoria>(this.ApiUrl);
  };
}
