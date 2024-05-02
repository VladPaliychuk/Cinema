import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {UserLoginModel} from "../../core/models/userlogin.model";
import {User} from "../../core/models/user.model";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5004/api/v1/User';

  constructor(private http: HttpClient) { }

  registerUser(user: User): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/Register`, user);
  }

  loginUser(login: UserLoginModel): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/Login`, login);
  }

  getAllUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GetAll`);
  }

  getUserById(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/GetUserById/${id}`);
  }

  createUser(user: any): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/CreateUser`, user);
  }

  updateUser(user: any): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/UpdateUser`, user);
  }

  deleteUser(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeleteUser/${id}`);
  }
}
