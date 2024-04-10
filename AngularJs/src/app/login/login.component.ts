import { Component } from '@angular/core';
import { AuthService } from '../services/authService/auth.service';
import { Router } from '@angular/router';
import {BasketService} from "../basket/basket.service";
import {catchError, tap, throwError} from "rxjs";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router, private basketService: BasketService) { }

  onSubmit(): void {
    if (this.authService.login(this.username, this.password)) {
      this.router.navigate(['/home']); // Navigate to home page

      this.basketService.createBasket(this.username).pipe(
        tap(() => console.log('Basket created for user:', this.username)),
        catchError(error => {
          console.error('Error creating basket:', error);
          return throwError(error); // Повертаємо потік помилок
        })).subscribe();
    }
    else {
      this.errorMessage = 'Invalid username or password'; // Show error message
    }
  }
}
