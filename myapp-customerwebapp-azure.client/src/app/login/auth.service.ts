import { HttpClient } from "@angular/common/http";
import { Observable, tap } from "rxjs";
import { Customer } from "../shared/models/customer.model";
import { Common } from '../shared/constants/common.constant';
import { Login } from "../shared/models/login.model";
import { Injectable } from "@angular/core";
import { RegisterRequest } from "../shared/models/register.model";
import { User } from "../shared/models/user.model";
@Injectable()
export class AuthService {

  private readonly baseUrl: string;
  private readonly ACCESS_TOKEN_KEY = 'access_token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';

  constructor(private client: HttpClient) {
    this.baseUrl = Common.BaseUrl;
  }

  login(login: Login) {
    let url = `${this.baseUrl}/account/login`;
    return this.client.post<Observable<Customer[]>>(url, login).pipe(
      tap((response: any) => {
        this.saveTokens(response.accessToken, response.refreshToken);
      })
    );;
  }

  register(data: RegisterRequest) {
    let url = `${this.baseUrl}/account/register`;
    return this.client.post(url, data);
  }

  logout() {
    localStorage.removeItem(this.ACCESS_TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
  }

  getToken(): string | null {
    return localStorage.getItem(this.ACCESS_TOKEN_KEY);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(this.REFRESH_TOKEN_KEY);
  }

  getProfile(): Observable<User> {
    let url = `${this.baseUrl}/account/profile`;
    return this.client.get<User>(url);
  }

  public saveTokens(accessToken: string, refreshToken: string): void {
    localStorage.setItem(this.ACCESS_TOKEN_KEY, accessToken);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, refreshToken);
  }

  //  private decodeToken(token: string) {
  //  try {
  //    return jwt_decode(token);
  //  } catch (error) {
  //    console.error('Invalid token', error);
  //    return null;
  //  }
  //}

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  public refreshToken(refreshToken: string): Observable<{ accessToken: string, refreshToken: string }> {
    const url = `${this.baseUrl}/account/refresh`;

    return this.client.post<{ accessToken: string, refreshToken: string }>(url, { refreshToken }).pipe(
      tap(response => {
        this.saveTokens(response.accessToken, response.refreshToken);
      })
    );
  }

}
