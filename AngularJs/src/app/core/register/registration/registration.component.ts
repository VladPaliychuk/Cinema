import { Component } from '@angular/core';
import {User} from "../../models/user.model";
import {AuthService} from "../../../services/authService/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  user: User = {
    username: '',
    password: '',
    email: '',
    role: 'user'
  };
  errorMessage: string = '';
  confirmPassword = '';
  passwordStrength = '';

  constructor(private authService: AuthService, private router: Router) { }

  onSubmit(): void {
    this.authService.registerUser(this.user).subscribe(
      success => {
        if (success) {
          this.authService.loginUser(this.user).subscribe(
            loginSuccess => {
              if (loginSuccess) {
                this.router.navigate(['/home']); // Navigate to home page
              } else {
                this.errorMessage = 'Automatic login failed'; // Show error message
              }
            },
            loginError => console.error('Error logging in:', loginError)
          );
        } else {
          this.errorMessage = 'Registration failed'; // Show error message
        }
      },
      error => console.error('Error registering:', error)
    );
  }

  checkPasswordStrength(): void {
    const password = this.user.password;
    const passwordLength = password.length;

    if (passwordLength < 8) {
      this.passwordStrength = 'small';
    } else if (passwordLength > 30) {
      this.passwordStrength = 'too big';
    } else if (/[a-zA-Z]/.test(password) && !/[0-9]/.test(password) && !/\W|_/.test(password)) {
      this.passwordStrength = 'weak';
    } else if (/[a-zA-Z]/.test(password) && /[0-9]/.test(password) && !/\W|_/.test(password)) {
      this.passwordStrength = 'medium';
    } else if (/[a-zA-Z]/.test(password) && /[0-9]/.test(password) && /\W|_/.test(password)) {
      this.passwordStrength = 'strong';
    }
  }
}
