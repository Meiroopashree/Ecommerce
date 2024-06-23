import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cart, CartItem, Product } from './models';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'https://8080-bfdeeddcedfabcfacbdcbaeadbebabcdebdca.premiumproject.examly.io/api/cart';

  constructor(private http: HttpClient) {}

  getCart(): Observable<{ cart: Cart; totalPrice: number }> {
    return this.http.get<{ cart: Cart; totalPrice: number }>(this.apiUrl);
  }

  addToCart(productId: number, quantity: number = 1): Observable<{ cart: Cart; totalPrice: number }> {
    return this.http.post<{ cart: Cart; totalPrice: number }>(`${this.apiUrl}/${productId}?quantity=${quantity}`, {});
  }

  removeFromCart(productId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${productId}`);
  }
}
