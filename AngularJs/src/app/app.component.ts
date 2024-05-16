import {Component} from '@angular/core';
import {AuthService} from "./services/authService/auth.service";
//import '~bootstrap/dist/css/bootstrap-theme.css';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(protected authService: AuthService) {
  }
}
