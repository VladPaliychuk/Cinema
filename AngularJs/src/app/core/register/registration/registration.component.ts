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
}
