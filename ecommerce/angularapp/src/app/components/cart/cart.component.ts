import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { AuthService } from '../../services/auth.service';
import { Cart } from 'src/app/models/cart';
import { CartProduct } from 'src/app/models/cart-product';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: Cart | undefined;
  loading: boolean = true;

  constructor(
    private cartService: CartService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    if (this.authService.isAuthenticated()) {
      const userId = this.authService.getUserId();
      if (userId) {
        this.cartService.getCart(userId).subscribe(
          (data) => {
            this.cart = data;
            this.loading = false;
          },
          (error) => {
            console.error('Error fetching cart', error);
            this.loading = false;
          }
        );
      }
    } else {
      console.error('User not authenticated');
      this.loading = false;
    }
  }

  addToCart(product: CartProduct): void {
    if (this.authService.isAuthenticated()) {
      const userId = this.authService.getUserId();
      if (userId) {
        const productWithUserId = { ...product, userId };
        this.cartService.addToCart(productWithUserId).subscribe(
          (data) => {
            this.cart = data;
            this.loading = false;
          },
          (error) => {
            console.error('Error adding to cart', error);
            this.loading = false;
          }
        );
      }
    } else {
      console.error('User not authenticated');
    }
  }

  // updateQuantity(item: any, quantity: number): void {
  //   item.quantity = quantity;
  //   this.cartService.updateCart(item).subscribe(
  //     (data) => {
  //       this.cart = data;
  //     },
  //     (error) => {
  //       console.error('Error updating quantity', error);
  //     }
  //   );
  // }

  // removeFromCart(item: any): void {
  //   this.cartService.removeFromCart(item).subscribe(
  //     (data) => {
  //       this.cart = data;
  //     },
  //     (error) => {
  //       console.error('Error removing from cart', error);
  //     }
  //   );
  // }

  quantityOptions(stockQuantity: number): number[] {
    return Array.from({ length: stockQuantity }, (_, i) => i + 1);
  }
}
