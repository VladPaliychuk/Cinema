import {Component, OnInit} from '@angular/core';
import {UserCard} from "../../../models/usercard.model";
import {AuthGuard} from "../../../../services/authService/auth.guard";
import {AuthService} from "../../../../services/authService/auth.service";
import {UserCardService} from "../../../../services/usercard/usercard.service";
import {UserService} from "../../../../services/user/user.service";

@Component({
  selector: 'user-office',
  templateUrl: './user-office.component.html',
  styleUrl: './user-office.component.css'
})
export class UserOfficeComponent implements OnInit{
  usercard!: UserCard;

  constructor(private auth: AuthService,
              private cardService: UserCardService,
              private userService: UserService) {}

  ngOnInit(): void {
    this.cardService.getCardByUsername(this.auth.getUsername()).subscribe(
      usercard => {
        this.usercard = usercard;
      },
      error => {
        console.error('Error fetching user card:', error);
      }
    );
  }

  updatePassword(newPassword: string): void {
    if (newPassword) {
      this.userService.updatePassword(this.auth.getUsername(), newPassword).subscribe(
        () => {
          alert('Пароль успішно оновлено!');
        },
        error => {
          console.error('Error updating password:', error);
        }
      );
    }
  }
}
