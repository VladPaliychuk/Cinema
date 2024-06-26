import { Component } from '@angular/core';
import {AuthService} from "../../services/authService/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  constructor(protected authService: AuthService, private router: Router) { }

  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  onCreate(): void {
    this.router.navigate(['/admin-office']);
  }

  protected readonly onclick = onclick;
}
