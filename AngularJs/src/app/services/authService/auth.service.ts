import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private user: {username: string, password: string} | null = null;
  private sessionTimeout: any = null;
  private sessionTimeoutInterval: number = 30 * 60 * 1000; // 30 minutes

  login(username: string, password: string): boolean {

    localStorage.setItem('user', JSON.stringify({username, password}));
    //this.user = {username, password};
    this.startSessionTimeout();
    return true; // return true if login is successful
  }

  logout(): void {
    //this.user = null;
    localStorage.removeItem('user');
    this.clearSessionTimeout
  }

  getUser(): {username: string, password: string} | null {
    this.resetSessionTimeout();
    //return this.user;
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  }

  isLoggedIn(): boolean {
    this.resetSessionTimeout();
    //return this.user !== null;
    return localStorage.getItem('user') !== null;
  }

  private startSessionTimeout(): void {
    this.sessionTimeout = setTimeout(() => this.logout(), this.sessionTimeoutInterval);
  }

  private clearSessionTimeout(): void {
    if (this.sessionTimeout) {
      clearTimeout(this.sessionTimeout);
      this.sessionTimeout = null;
    }
  }

  private resetSessionTimeout(): void {
    this.clearSessionTimeout();
    this.startSessionTimeout();
  }
}
