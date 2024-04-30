import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie } from '../../core/models/movie.model';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  private baseUrl = 'http://localhost:5000/api/v1/Catalog';

  constructor(private http: HttpClient) { }

  getMovie(productName: string | null): Observable<Movie> {
    if (!productName) {
      // Handle the case where productName is null or undefined
      // For example, you could return an Observable.throw() or handle it in a different way
      throw new Error('productName cannot be null or undefined');
    }

    return this.http.get<Movie>(`${this.baseUrl}/GetProductDetails/${productName}`);
  }


  createMovie(movie: Movie): Observable<Movie> {
    return this.http.post<Movie>(`${this.baseUrl}/CreateProductDetail`, movie);
  }
}
