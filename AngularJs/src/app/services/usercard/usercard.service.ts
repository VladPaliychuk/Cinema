import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {UserCard} from "../../core/models/usercard.model";

@Injectable({
  providedIn: 'root'
})
export class UserCardService {
  private apiUrl = 'http://localhost:5003/api/v1/Cards'; // Replace with your API endpoint

  constructor(private http: HttpClient) { }

  getAllUserCards(): Observable<UserCard[]> {
    return this.http.get<UserCard[]>(`${this.apiUrl}/GetAllUserCards`);
  }

  getCardById(id: string): Observable<UserCard> {
    return this.http.get<UserCard>(`${this.apiUrl}/GetCardById/${id}`);
  }

  getCardByUsername(username: string): Observable<UserCard> {
    return this.http.get<UserCard>(`${this.apiUrl}/GetCardByUsername/${username}`);
  }

  getCardsByCountry(country: string): Observable<UserCard[]> {
    return this.http.get<UserCard[]>(`${this.apiUrl}/GetCardsByCountry/${country}`);
  }

  createUserCard(card: UserCard): Observable<UserCard> {
    return this.http.post<UserCard>(`${this.apiUrl}/CreateCard`, card);
  }

  updateUserCard(card: UserCard): Observable<UserCard> {
    return this.http.put<UserCard>(`${this.apiUrl}/UpdateCard`, card);
  }

  deleteUserCard(username: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteCard/${username}`);
  }
}
