import { Component } from '@angular/core';
import { AuthService } from '../../services/authService/auth.service';
import { Router } from '@angular/router';
import { UserLoginModel } from "../models/userlogin.model";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  onSubmit(): void {
    // Form validation
    if (!this.username || !this.password) {
      this.errorMessage = 'Please enter both username and password.';
      return;
    }

    const login: UserLoginModel = {
      username: this.username,
      password: this.password
    };

    console.log('Logging in with:', login);

    this.authService.loginUser(login).subscribe(
      success => {
        console.log('Login success:', success);
        if (success) {
          const redirectUrl = '/catalog';
          this.router.navigate([redirectUrl]);
        } else {
          this.errorMessage = 'Invalid username or password';
        }
      },
      error => {
        console.error('Error logging in:', error);
        this.errorMessage = 'An error occurred while logging in. Please try again later.';
      }
    );
  }
}
