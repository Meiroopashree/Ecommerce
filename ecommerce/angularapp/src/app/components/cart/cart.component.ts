import { Component, OnInit } from '@angular/core';
import { CartService } from './';
import { Cart } from 'src/app/models/cart';
import { CartProduct } from 'src/app/models/cart-product';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: Cart | undefined;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    // Replace '1' with the actual cart ID
    this.cartService.getCart(1).subscribe(
      (data) => {
        this.cart = data;
      },
      (error) => {
        console.error('Error fetching cart', error);
      }
    );
  }

  addToCart(product: CartProduct): void {
    this.cartService.addToCart(product).subscribe(
      (data) => {
        this.cart = data;
      },
      (error) => {
        console.error('Error adding to cart', error);
      }
    );
  }
}
