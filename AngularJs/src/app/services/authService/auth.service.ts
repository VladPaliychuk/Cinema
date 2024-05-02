import { Injectable } from '@angular/core';
import { UserService } from "../user/user.service";
import { Observable, tap } from "rxjs";
import { User } from "../../core/models/user.model";
import { UserLoginModel } from "../../core/models/userlogin.model";
import { Router } from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private userService: UserService, private router: Router) { }
  public username = '';
  private sessionTimeout: any = null;
  private sessionTimeoutInterval: number = 30 * 60 * 1000; // 30 minutes

  loginUser(login: UserLoginModel): Observable<boolean> {
    return this.userService.loginUser(login).pipe(
      tap(success => {
        if (success) {
          console.log('Login success:', success);
          this.username = login.username;
          this.startSessionTimeout();
          localStorage.setItem('token', 'true');
        }
      })
    );
  }

  registerUser(user: User): Observable<boolean> {
    return this.userService.registerUser(user).pipe(
      tap(success => {
        if (success) {
          this.startSessionTimeout();
          localStorage.setItem('token', 'true');
        }
      })
    );
  }

  logout(): void {
    console.log('Logging out')
    localStorage.removeItem('token'); // Clear the authentication token
    this.clearSessionTimeout(); // Clear session timeout
    this.router.navigate(['/login']); // Redirect to login page
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token'); // Check if authentication token exists
  }

  private startSessionTimeout(): void {
    this.clearSessionTimeout();
    this.sessionTimeout = setTimeout(() => this.logout(), this.sessionTimeoutInterval);
  }

  private clearSessionTimeout(): void {
    if (this.sessionTimeout) {
      clearTimeout(this.sessionTimeout);
      this.sessionTimeout = null;
    }
  }
}
