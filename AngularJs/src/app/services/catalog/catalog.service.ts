import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {catchError, map, Observable, throwError} from 'rxjs';
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

  searchProductsLocally(searchTerm: string): Observable<Product[]> {
    // Отримати всі продукти з сервера (можна також використати поточний список продуктів з пам'яті)
    return this.getProducts().pipe(
      map(products => {
        // Фільтрація продуктів за введеним терміном
        return products.filter(product =>
          product.name.toLowerCase().includes(searchTerm.toLowerCase())
        );
      })
    );
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

  getSortedScreeningsAndMoviesByDateTime(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/GetSortedScreeningsAndMoviesByDateTime`);
  }

  getAllScreenings(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/GetAllScreenings`);
  }

  getAllScreeningsWithSeats(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/GetAllScreeningsWithSeats`);
  }

  getScreeningWithSeatsById(id: string | null): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/GetScreeningWithSeatsById/${id}`);
  }

  getProductById(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/GetProductById/${id}`);
  }

  reserveSeat(screeningId: string, seatId: string, username: string) {
    return this.http
      .post(`${this.baseUrl}/ReserveSeat/${screeningId}/${seatId}`, { username }, { responseType: 'blob' })
      .pipe(
        catchError(error => {
          console.error('Error:', error);
          return throwError('An error occurred while reserving the seat.');
        })
      );
  }

  createProductScreeningRelation(productName: string, screeningDate: string, screeningTime: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/CreateProductScreeningRelation`, { productName, screeningDate, screeningTime });
  }

  deleteProductScreeningRelation(productName: string, scrDate: string, scrTime: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteProductScreeningRelation?productName=${productName}&scrDate=${scrDate}&scrTime=${scrTime}`);
  }

  deleteScreening(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteScreening/${id}`);
  }

  deleteScreeningByDateTime(screeningDate: string, screeningTime: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteScreeningByDateTime?screeningDate=${screeningDate}&screeningTime=${screeningTime}`);
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
