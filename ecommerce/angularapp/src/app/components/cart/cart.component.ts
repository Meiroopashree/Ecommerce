import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
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

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    // Replace '1' with the actual cart ID or fetch cart based on user authentication
    this.cartService.getCart(1).subscribe(
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

  updateQuantity(item: CartProduct, newQuantity: number): void {
    item.quantity = newQuantity;

    this.cartService.updateCartItem(item.product.productId, item).subscribe(
      (data) => {
        this.cart = data;
      },
      (error) => {
        console.error('Error updating quantity', error);
      }
    );
  }

  removeFromCart(item: CartProduct): void {
    this.cartService.removeFromCart(item.product.productId).subscribe(
      (data) => {
        this.cart = data;
      },
      (error) => {
        console.error('Error removing item from cart', error);
      }
    );
  }

  calculateTotalPrice(): number {
    if (!this.cart) {
      return 0;
    }
    return this.cart.items.reduce((total, item) => total + item.quantity * item.product.price, 0);
  }
}
