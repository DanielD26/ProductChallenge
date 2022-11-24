import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';
import { Order } from '../models/order';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  readonly baseUrl: string = 'https://danielchallengeapi.azurewebsites.net/'
  constructor(private _http: HttpClient) { }

  getCustomers(): Observable<Customer[]> {
    const url = this.baseUrl + "customer"

    return this._http.get<Customer[]>(url);
  }

  getProduct(): Observable<Product[]> {
    const url = this.baseUrl + "product"

    return this._http.get<Product[]>(url);
  }

  getOrders(): Observable<Order[]> {
    const url = this.baseUrl + "order"

    return this._http.get<Order[]>(url);
  }

  postOrder(order: Order): Observable<Order> {
    return this._http.post<Order>(this.baseUrl + "order", order)
  }

  putOrder(order: Order): Observable<string> {
    const headers = { 'content-type': 'application/json'};
    const body = JSON.stringify(order);
    return this._http.put<string>(this.baseUrl + "order", body, {'headers': headers})
  }

  deleteOrder(order: Order): Observable<any> {
    const headers = { 'content-type': 'application/json'};
    const body = JSON.stringify(order);
    return this._http.delete<any>(this.baseUrl + "order", {body: body, 'headers': headers})
  }
}
