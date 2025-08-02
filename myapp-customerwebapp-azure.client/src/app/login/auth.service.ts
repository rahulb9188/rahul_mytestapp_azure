import { HttpClient } from "@angular/common/http";
import { Observable, tap } from "rxjs";
import { Customer } from "../shared/models/customer.model";
import { Common } from '../shared/constants/common.constant';
import { Login } from "../shared/models/login.model";
import { Injectable } from "@angular/core";
@Injectable()
export class AuthService {

    private readonly baseUrl: string;

    constructor(private client: HttpClient) {
        this.baseUrl = Common.BaseUrl;
    }

    login(login: Login) {
        let url = `${this.baseUrl}/account/login`;
        return this.client.post<Observable<Customer[]>>(url, login).pipe(
            tap((response: any) => {
                localStorage.setItem('authToken', response.token); // Store the token
            })
        );;
    }

    logout() {
        localStorage.removeItem('authToken');
    }

    getToken(): string | null {
        return localStorage.getItem('authToken');
    }

    isAuthenticated(): boolean {
        return !!this.getToken();
    }

}
