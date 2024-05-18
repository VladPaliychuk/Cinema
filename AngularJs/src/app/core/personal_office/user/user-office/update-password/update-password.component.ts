import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../../../services/user/user.service';
import { AuthService } from '../../../../../services/authService/auth.service';

@Component({
  selector: 'update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.css']
})
export class UpdatePasswordComponent implements OnInit {
  password = '';
  oldPassword = '';
  newPassword = '';
  confirmPassword = '';
  passwordStrength = '';
  passwordCheck = '';

  constructor(private auth: AuthService, private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUserByUsername(this.auth.getUsername()).subscribe(
      user => {
        this.password = user.password;
      },
      error => {
        console.error('Error getting user:', error);
      }
    );
  }

  updatePassword(): void {
    if (this.newPassword !== this.confirmPassword) {
      this.passwordCheck = 'Passwords do not match';
      return;
    }

    this.userService.updatePassword(this.auth.getUsername(), this.newPassword).subscribe(
      () => {
        console.log('Password successfully updated!');
      },
      error => {
        console.error('Error updating password:', error);
      }
    );
  }

  checkSimilarity(): void {
    if (this.password !== this.oldPassword) {
      this.passwordCheck = 'Old password is incorrect';
    } else {
      this.passwordCheck = '';
    }
  }

  checkSimilarityNewPassword(): void {
    if (this.newPassword !== this.confirmPassword) {
      this.passwordCheck = 'Passwords do not match';
    } else {
      this.passwordCheck = '';
    }
  }

  checkPasswordStrength(): void {
    const password = this.newPassword;
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
