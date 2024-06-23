import { Product } from './product';

  export interface CartItem {
    cartItemId: number;
    productId: number;
    quantity: number;
    product: Product;
  
}
