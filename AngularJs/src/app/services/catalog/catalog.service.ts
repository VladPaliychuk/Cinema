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
    return this.http.get<Product[]>(`${this.baseUrl}/GetAllProducts`);
  }

  getProductsByActor(actor: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/GetProductsByActorName/${actor}`);
  }
  getProductsByDirector(director: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/GetProductsByDirectorName/${director}`);
  }
  getProductsByGenre(genre: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/GetProductsByGenre/${genre}`);
  }

  createProductActorRelation(productName: string, actorName: string): Observable<void> {
    const url = `${this.baseUrl}/CreateProductActorRelation?productName=${encodeURIComponent(productName)}&actorName=${encodeURIComponent(actorName)}`;
    return this.http.post<void>(url, {});
  }

  createProductGenreRelation(productName: string, genreName: string): Observable<void> {
    const url = `${this.baseUrl}/CreateProductGenreRelation?productName=${encodeURIComponent(productName)}&genreName=${encodeURIComponent(genreName)}`;
    return this.http.post<void>(url, {});
  }

  createProductDirectorRelation(productName: string, directorName: string): Observable<void> {
    const url = `${this.baseUrl}/CreateProductDirectorRelation?productName=${encodeURIComponent(productName)}&directorName=${encodeURIComponent(directorName)}`;
    return this.http.post<void>(url, {});
  }

  deleteProductActorRelation(productName: string, actorName: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteProductActorRelation?productName=${productName}&actorName=${actorName}`);
  }

  deleteProductGenreRelation(productName: string, genreName: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteProductGenreRelation?productName=${productName}&genreName=${genreName}`);
  }

  deleteProductDirectorRelation(productName: string, directorName: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteProductDirectorRelation?productName=${productName}&directorName=${directorName}`);
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
