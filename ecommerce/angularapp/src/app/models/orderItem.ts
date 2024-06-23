// src/app/models/orderItem.ts
import { Product } from './product';

export interface OrderItem {
  id: number;
  productId: number;
  quantity: number;
  product: Product; // Include Product entity to match backend
}
