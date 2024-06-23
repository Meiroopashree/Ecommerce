// import { Component, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { CartProduct } from 'src/app/models/cart-product';
// import { Product } from 'src/app/models/product';
// import { CartService } from 'src/app/services/cart.service';
// import { ProductService } from 'src/app/services/product.service';

// @Component({
//   selector: 'app-customer-view-product',
//   templateUrl: './customer-view-product.component.html',
//   styleUrls: ['./customer-view-product.component.css']
// })
// export class CustomerViewProductComponent{

//   products: Product[] = [];

//   constructor(private productService: ProductService, private router: Router,  private cartService: CartService) {}

//   ngOnInit(): void {
//     this.loadProducts();
//   }

//   loadProducts(): void {
//     this.productService.getProducts().subscribe(products => {
//       this.products = products;
//     }, error => {
//       console.error('Error fetching products:', error);
//     });
//   }

//   addToCart(product: Product): void {
//     const cartProduct: CartProduct = {
//       product: product,
//       quantity: 1
//     };

//     this.cartService.addToCart(cartProduct).subscribe(
//       (data) => {
//         this.router.navigate(['/cart']); // Navigate to the cart page
//       },
//       (error) => {
//         console.error('Error adding to cart', error);
//       }
//     );
//   }

// }


import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';
import { CartItem } from 'src/app/models/cartItem';

@Component({
  selector: 'app-customer-view-product',
  templateUrl: './customer-view-product.component.html',
  styleUrls: ['./customer-view-product.component.css']
})
export class CustomerViewProductComponent implements OnInit {

  products: Product[] = [];

  constructor(private productService: ProductService, private router: Router, private cartService: CartService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe(products => {
      this.products = products;
    }, error => {
      console.error('Error fetching products:', error);
    });
  }

  addToCart(product: Product): void {
    const cartItem: CartItem = {
      cartItemId: 0, // This will be set by the backend
      productId: product.productId,
      quantity: 1,
      product: product
    };

    this.cartService.addToCart(cartItem.productId, cartItem.quantity).subscribe(
      () => {
        this.router.navigate(['/cart']); // Navigate to the cart page
      },
      (error) => {
        console.error('Error adding to cart', error);
      }
    );
  }
}
