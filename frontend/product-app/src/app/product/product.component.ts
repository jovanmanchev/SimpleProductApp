import { Component, OnInit } from '@angular/core';
import { ProductService, Product } from '../product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  products: Product[] = [];
  product: Product = { name: '', description: '', price: 0 }; 

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.getProducts()
  }

  getProducts(): void {
    this.productService.getProducts().subscribe(products => this.products = products);
  }

  addProduct(): void {
    if (!this.product.name) return;
    this.product.price = +this.product.price; 
    this.productService.createProduct(this.product).subscribe(product => {
      this.products.push(product);
      this.product = { name: '', description: '', price: 0 };
    });
  }

  deleteProduct(id: string | undefined): void {
    if(id){
      this.productService.deleteProduct(id).subscribe(() => {
        this.products = this.products.filter(p => p.id !== id);
      });
    }
   
  }
}
