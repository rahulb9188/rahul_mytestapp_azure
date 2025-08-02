import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Customer } from "../shared/models/customer.model";
import {Common} from '../shared/constants/common.constant';
import { Injectable } from "@angular/core";

@Injectable({
    providedIn:"root"
})
export class DashboardService {

    private readonly baseUrl:string;

    constructor(private client:HttpClient){
        this.baseUrl=Common.BaseUrl;
    }

    getCustomers() {
        return this.client.get<Customer[]>(`${this.baseUrl}/dashboard/loadCustomers`);
    }

}