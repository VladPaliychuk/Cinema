import {Component, OnInit} from '@angular/core';
import {UserService} from "../../../../../services/user/user.service";
import {AuthService} from "../../../../../services/authService/auth.service";
import { User } from '../../../../models/user.model';

@Component({
  selector: 'update-password',
  templateUrl: './update-password.component.html',
  styleUrl: './update-password.component.css'
})
export class UpdatePasswordComponent implements OnInit{
  confirmPassword = '';
  user!: User;
  passwordStrength = '';
  passwordCheck = '';

  constructor(private auth: AuthService,
              private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getUserByUsername(this.auth.getUsername()).subscribe(
      user => {
        this.user = user;
        console.log('User:', user);
      },
      error => {
        console.error('Error fetching user:', error);
      }
    );
  }

  updatePassword(newPassword: string): void {
      this.userService.updatePassword(this.auth.getUsername(), newPassword).subscribe(
        () => {
          console.log('Пароль успішно оновлено!');
        },
        error => {
          console.error('Error updating password:', error);
        }
      );
  }
  checkPassword(): void {
    if (this.user.password !== this.confirmPassword) {
      this.passwordCheck = 'Паролі не співпадають';
    }
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
