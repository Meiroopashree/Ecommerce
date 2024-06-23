// src/app/models/order.ts
import { OrderItem } from './orderItem';

export interface Order {
  id: number;
  items: OrderItem[];
  totalPrice: number;
  orderDate: Date;
  // Add other necessary properties if needed (e.g., customer details, payment status)
}
