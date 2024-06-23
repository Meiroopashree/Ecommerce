// src/app/components/order/order.component.ts
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Order } from 'src/app/models/order';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  order: Order;

  constructor(
    private cartService: CartService,
    private orderService: OrderService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadOrderDetails();
  }

  loadOrderDetails(): void {
    this.cartService.getCart().subscribe(response => {
      const cart = response.cart; // Access the cart object
      this.order = {
        id: 0,
        items: cart.items.map(item => ({
          id: 0,
          productId: item.productId,
          quantity: item.quantity,
          product: item.product
        })),
        totalPrice: response.totalPrice,
        orderDate: new Date()
      };
    }, error => {
      console.error('Error fetching cart details:', error);
    });
  }

  placeOrder(): void {
    this.orderService.placeOrder(this.order).subscribe(order => {
      alert('Payment successful');
      this.router.navigate(['/']); // Redirect to home or another page
    }, error => {
      console.error('Error placing order:', error);
    });
  }
}
