import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { Cart } from 'src/app/models/cart';
import { CartProduct } from 'src/app/models/cart-product';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: Cart | undefined;

  constructor(
    private cartService: CartService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      const userId = this.authService.getUserId();
      if (userId) {
        this.cartService.getCart(userId).subscribe(
          (data) => {
            this.cart = data;
          },
          (error) => {
            console.error('Error fetching cart', error);
          }
        );
      }
    } else {
      console.error('User not authenticated');
    }
  }

  addToCart(product: CartProduct): void {
    if (this.authService.isAuthenticated()) {
      const userId = this.authService.getUserId();
      if (userId) {
        product.userId = userId; // Assuming userId needs to be sent with the product
        this.cartService.addToCart(product).subscribe(
          (data) => {
            this.cart = data;
          },
          (error) => {
            console.error('Error adding to cart', error);
          }
        );
      }
    } else {
      console.error('User not authenticated');
    }
  }
}
