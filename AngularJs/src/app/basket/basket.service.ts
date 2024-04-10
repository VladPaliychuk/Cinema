// src/app/basket/basket.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShoppingCart } from './shopping-cart.model'; // Import your ShoppingCart model

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private baseUrl = 'http://localhost:5001/api/v1/Basket'; // Update this to your actual API URL

  constructor(private http: HttpClient) { }

  createBasket(userName: string): Observable<ShoppingCart> {
    const newBasket: ShoppingCart = { userName, items: [] };
    return this.http.post<ShoppingCart>(this.baseUrl, newBasket);
  }

  getBasket(userName: string): Observable<ShoppingCart> {
    return this.http.get<ShoppingCart>(`${this.baseUrl}/${userName}`);
  }

  updateBasket(basket: ShoppingCart | undefined | null): Observable<ShoppingCart> {
    return this.http.post<ShoppingCart>(this.baseUrl, basket);
  }

  deleteBasket(userName: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${userName}`);
  }
}
