import { Component } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {
  product: Product = {
    productId: 0,
    name: '',
    description: '',
    price: 0,
    imageUrl: '',
    stockQuantity: 0,
    category: ''
  };

  constructor(private productService: ProductService, private router: Router) { }

  onSubmit(): void {
    this.productService.addProduct(this.product).subscribe(
      response => {
        console.log('Product added successfully:', response);
        this.resetForm();
        this.router.navigate(['admin/viewProducts']);
      },
      error => {
        console.error('Error adding product:', error);
      }
    );
  }

  resetForm(): void {
    this.product = {
      productId: 0,
      name: '',
      description: '',
      price: 0,
      imageUrl: '',
      stockQuantity: 0,
      category: ''
    };
  }
}
