import { CartProduct } from './cart-product';

export interface Cart {
  cartId: number;
  userId: string;
  items: CartProduct[];
  totalPrice: number;
}
