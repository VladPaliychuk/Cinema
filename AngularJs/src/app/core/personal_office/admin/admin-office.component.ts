import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../../services/authService/auth.service";
import { Router } from "@angular/router";
import { CatalogService } from "../../../services/catalog/catalog.service";
import { Product } from "../../models/product.model";
import { UserCard } from "../../models/usercard.model";
import { UserCardService } from "../../../services/usercard/usercard.service";

@Component({
  selector: 'app-admin-office',
  templateUrl: './admin_office.component.html',
  styleUrls: ['./admin_office.component.css']
})
export class AdminOfficeComponent implements OnInit {
  products: Product[] = [];
  usercards: UserCard[] = [];
  editMode: { [key: string]: boolean } = {};
  isCreating: boolean = false;
  newUserCard: UserCard = {
    userName: '',
    bonuses: 0,
    firstName: '',
    lastName: '',
    emailAddress: '',
    addressLine: '',
    country: '',
    state: '',
    zipCode: ''
  };

  constructor(
    private authService: AuthService,
    private router: Router,
    private catalogService: CatalogService,
    private udService: UserCardService
  ) {}

  ngOnInit(): void {
    this.getProducts();
    this.getUserCards();
  }

  create_movie(): void {
    this.router.navigate(['/movie-create']);
  }

  toggleCreateUserCard(): void {
    this.isCreating = !this.isCreating;
  }

  createUserCard(): void {
    this.udService.createUserCard(this.newUserCard).subscribe(
      () => {
        this.getUserCards();
        this.isCreating = false;
        this.resetNewUserCard();
      },
      error => console.error('Error creating user card:', error)
    );
  }

  resetNewUserCard(): void {
    this.newUserCard = {
      userName: '',
      bonuses: 0,
      firstName: '',
      lastName: '',
      emailAddress: '',
      addressLine: '',
      country: '',
      state: '',
      zipCode: ''
    };
  }

  getProducts(): void {
    this.catalogService.getProducts().subscribe(
      products => this.products = products,
      error => console.error('Error fetching movies:', error)
    );
  }

  getUserCards(): void {
    this.udService.getAllUserCards().subscribe(
      usercards => {
        this.usercards = usercards;
        this.usercards.forEach(usercard => console.log(usercard.userName));
      },
      error => console.error('Error fetching usercards:', error)
    );
  }

  delete_movie(id: string): void {
    this.catalogService.deleteProduct(id).subscribe(
      () => this.getProducts(),
      error => console.error('Error deleting movie:', error)
    );
  }

  delete_card(username: string): void {
    this.udService.deleteUserCard(username).subscribe(
      () => this.getUserCards(),
      error => console.error('Error deleting card:', error)
    );
  }

  enableEditMode(card: UserCard): void {
    this.editMode[card.userName] = true;
  }

  saveChanges(card: UserCard): void {
    this.udService.updateUserCard(card).subscribe(
      () => {
        this.editMode[card.userName] = false;
        this.getUserCards();
      },
      error => console.error('Error updating card:', error)
    );
  }

  cancelEdit(card: UserCard): void {
    this.editMode[card.userName] = false;
    this.getUserCards();
  }
}
