import { CartItem } from './cartItem';

export interface Cart {
  cartId: number;
  items: CartItem[];
}
