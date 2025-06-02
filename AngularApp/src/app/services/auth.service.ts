import { Inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { UserLogin } from '../models/UserLogin';
import { BehaviorSubject, catchError, Observable, of, take, tap, throwError } from 'rxjs';
import { User } from '../models/User';
import { LOCAL_STORAGE } from './storage.token';
import { Usuario } from '../models/Usuario';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  ApiURL = environment.UrlApi;
  private token = 'token';
  private refreshToken = 'refreshToken';
  private expiration = 'expiration';


  private isLoggedInSubject = new BehaviorSubject<boolean>(this.hasToken());

  constructor(
    private http: HttpClient,
    @Inject(LOCAL_STORAGE) private storage: Storage | null
  ) { }

  // 🟢 Login: Envia LoginDTO e salva o token
  public login(userLogin: UserLogin): Observable<UserLogin> {
    return this.http.post<UserLogin>(`${this.ApiURL}Auth/login`, userLogin).pipe(
      tap((response) => this.setToken(response.token, response.refreshToken, response.expiration))
    );
  } 

  public register(user: User): Observable<User> {
      return this.http.post<User>(`${this.ApiURL}Auth/register`, user).pipe(
          tap((response) => {
              // Supondo que você queira definir um token ou realizar alguma outra ação com a resposta
              this.setToken(response.email, response.userName, response.password);
          })
      );
  }

  public getUsuario(): Observable<Usuario>{
    return this.http.get<Usuario>(`${this.ApiURL}Auth/GetUsuario`).pipe(take(1));
  }

  public updateUsuario(userLogin: UserLogin): Observable<UserLogin> {
    return this.http.post<UserLogin>(`${this.ApiURL}Auth/updateUsuario`, userLogin).pipe(
      tap((response) => this.setToken(response.token, response.refreshToken, response.expiration))
    );
  }    

  // 🔴 Logout: Remove o token
  public logout(): void {
    this.storage?.removeItem(this.token);
    this.storage?.removeItem(this.refreshToken);
    this.storage?.removeItem(this.expiration);
    this.isLoggedInSubject.next(false);
  }

  // 🟡 Salva o token
  private setToken(token: string, refreshToken: string, expiration: string): void {
    this.storage?.setItem(this.token, token);
    this.storage?.setItem(this.refreshToken, refreshToken);
    this.storage?.setItem(this.expiration, expiration);
    this.isLoggedInSubject.next(true);
  }

  // 🔍 Recupera o token
  public getToken(): string | null {
    return this.storage?.getItem(this.token) || null;
  }

  // Pegar o refreshToken do this.storage?
  private getRefreshToken(): string | null {
    return this.storage?.getItem(this.refreshToken) || null;
  }

  // Tentar renovar o token
  public refreshTokenKey(): Observable<UserLogin | null> {
  const refreshToken = this.getRefreshToken();
  if (!refreshToken) return of(null);
  return this.http.post<UserLogin>(`${this.ApiURL}/refresh`, { refreshToken }).pipe(
    tap((response) => this.setToken(response.token, response.refreshToken, this.expiration)),
    catchError(() => {
      this.logout(); // Se falhar, faz logout
      return of(null);
    })
  );
  }

  // Observable para acompanhar estado de autenticação
  public get isLoggedIn$(): Observable<boolean> {
    return this.isLoggedInSubject.asObservable();
  }

  // ✅ Verifica se está autenticado
  // Verificar a existência do token no carregamento
  public isAuthenticated(): boolean {
    return !!this.getToken();
  }

  // Verificar a existência do token no carregamento
  private hasToken(): boolean {
    return !!this.getToken();
  }

}
