import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private user: {username: string, password: string} | null = null;

  login(username: string, password: string): boolean {
    // Validate the username and password
    // If valid, store the username and password
    this.user = {username, password};
    return true; // return true if login is successful
  }

  logout(): void {
    // Clear the stored username and password
    this.user = null;
  }

  getUser(): {username: string, password: string} | null {
    return this.user;
  }

  isLoggedIn(): boolean {
    return this.user !== null;
  }
}
