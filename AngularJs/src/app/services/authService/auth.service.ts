import { Injectable } from '@angular/core';
import { UserService } from "../user/user.service";
import { BehaviorSubject, Observable, ReplaySubject, tap, switchMap, catchError, of } from "rxjs";
import { User } from "../../core/models/user.model";
import { UserLoginModel } from "../../core/models/userlogin.model";
import { Router } from "@angular/router";

const SESSION_TIMEOUT_INTERVAL = 30 * 60 * 1000; // 30 minutes

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public username = '';
  public role = '';
  private sessionTimeout: any = null;

  private adminRole = new BehaviorSubject<boolean>(false);
  public isAdminRole = this.adminRole.asObservable();

  private loggedIn = new BehaviorSubject<boolean>(false);
  public isLoggedInAsync = this.loggedIn.asObservable();

  private readySource = new ReplaySubject<boolean>(1);
  public isReady = this.readySource.asObservable();

  constructor(private userService: UserService, private router: Router) {
    const username = localStorage.getItem('username');
    const role = localStorage.getItem('role');
    if (username && role) {
      this.username = username;
      this.role = role;
      this.adminRole.next(role === 'admin');
      this.setReady(true);
      this.loggedIn.next(true);
    }
  }

  private setReady(value: boolean) {
    this.readySource.next(value);
  }

  loginUser(login: UserLoginModel): Observable<boolean> {
    return this.userService.loginUser(login).pipe(
      switchMap(success => {
        if (success) {
          this.username = login.username;
          localStorage.setItem('username', login.username);
          this.setReady(true);
          this.loggedIn.next(true);
          this.startSessionTimeout();
          localStorage.setItem('token', 'true');

          return this.userService.getUserByUsername(this.username).pipe(
            tap(user => {
              if (user) {
                this.role = user.role;
                localStorage.setItem('role', user.role);
                this.adminRole.next(user.role === 'admin');
              }
            }),
            switchMap(() => of(true))
          );
        }
        return of(false);
      }),
      catchError(error => {
        // handle error
        console.error(error);
        return of(false);
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
      }),
      catchError(error => {
        // handle error
        console.error(error);
        return of(false);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    localStorage.removeItem('role');
    this.clearSessionTimeout();
    this.loggedIn.next(false);
    this.adminRole.next(false);
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  isAdmin(): boolean {
    return this.role === 'admin';
  }
  getUsername(): string {
    return this.username;
  }
  private startSessionTimeout(): void {
    this.clearSessionTimeout();
    this.sessionTimeout = setTimeout(() => this.logout(), SESSION_TIMEOUT_INTERVAL);
  }

  private clearSessionTimeout(): void {
    if (this.sessionTimeout) {
      clearTimeout(this.sessionTimeout);
      this.sessionTimeout = null;
    }
  }
}
