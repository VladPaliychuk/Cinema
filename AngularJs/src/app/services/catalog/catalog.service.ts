import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Product} from "../../core/models/product.model";

@Injectable({
  providedIn: 'root'
})
export class CatalogService {
  private baseUrl = 'http://localhost:5000/api/v1/Catalog';

  constructor(private http: HttpClient) { }

  getProductsPage(page: number, pageSize: number): Observable<Product[]> {
    const skip = (page - 1) * pageSize;
    const url = `${this.baseUrl}/GetTakeSkip?take=${pageSize}&skip=${skip}&sortBy=Id`;
    return this.http.get<Product[]>(url);
  }
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/GetAllProduct`);
  }

  getProductById(id: string): Observable<any> {
    return this.http.get<Product>(`${this.baseUrl}/GetProductByIdAsync/${id}`);
  }

  getProductsByName(name: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/GetProductsByName/${name}`);
  }

  getProductsByCategory(category: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/GetProductsByCategory/${category}`);
  }

  searchProducts(name?: string, category?: string): Observable<Product[]> {
    let url = `${this.baseUrl}/SearchProducts`;

    if(!name){name="none";}
    if(!category){category="none";}

    if (name || category) {
      url += '?';
      if (name) {
        url += `name=${name}`;
      }
      if (category) {
        url += '&';
        url += `category=${category}`;
      }
    }
    return this.http.get<Product[]>(url);
  }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(`${this.baseUrl}/CreateProductAsync`, product);
  }

  updateProduct(product: Product): Observable<Product> {
    return this.http.put<Product>(`${this.baseUrl}/UpdateProductAsync`, product);
  }

  deleteProduct(id: string): Observable<Product> {
    return this.http.delete<Product>(`${this.baseUrl}/DeleteProductAsync ${id}`);
  }
}
