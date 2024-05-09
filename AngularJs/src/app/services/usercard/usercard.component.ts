import {UserCard} from "../../core/models/usercard.model";
import {UserCardService} from "./usercard.service";
import {Component} from "@angular/core";

@Component({
  selector: 'app-usercard',
  templateUrl: './usercard.component.html',
  styleUrls: ['./usercard.component.css']
})
export class UserCardComponent{
  userCards!: UserCard[];

  constructor(private userCardService: UserCardService) { }

}
