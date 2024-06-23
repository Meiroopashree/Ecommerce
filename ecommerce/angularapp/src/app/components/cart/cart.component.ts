import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cart } from '../../models/cart';
import { CartService } from '../../services/cart.service';
import { CartItem } from 'src/app/models/cartItem';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: Cart | null = null;
  totalPrice: number = 0;

  constructor(private cartService: CartService, private router: Router) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.cartService.getCart().subscribe(response => {
      this.cart = response.cart;
      this.totalPrice = response.totalPrice;
    });
  }

  updateQuantity(item: CartItem, quantity: number): void {
    if (quantity < 1) {
      quantity = 1;
    }
    item.quantity = quantity;
    this.cartService.updateCartItemQuantity(item.cartItemId, quantity).subscribe(() => {
      this.loadCart();
    });
  }

  removeFromCart(productId: number): void {
    this.cartService.removeFromCart(productId).subscribe(() => {
      this.loadCart();
    });
  }
}
