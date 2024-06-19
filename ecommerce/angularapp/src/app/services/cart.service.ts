import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cart } from '../models/cart';
import { CartProduct } from '../models/cart-product';


@Injectable({
  providedIn: 'root'
})
export class CartService {
  private baseUrl = 'https://8080-bfdeeddcedfabcfacbdcbaeadbebabcdebdca.premiumproject.examly.io/api/cart'; // Update this to your actual API URL

  constructor(private http: HttpClient) {}

  getCart(cartId: number): Observable<Cart> {
    return this.http.get<Cart>(`${this.baseUrl}/${cartId}`);
  }

  addToCart(cartProduct: CartProduct): Observable<Cart> {
    return this.http.post<Cart>(`${this.baseUrl}/add`, cartProduct);
  }
}
