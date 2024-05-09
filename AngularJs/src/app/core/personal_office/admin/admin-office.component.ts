import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../../services/authService/auth.service";
import {Router} from "@angular/router";
import {CatalogService} from "../../../services/catalog/catalog.service";
import {Product} from "../../models/product.model";
import {UserService} from "../../../services/user/user.service";
import {UserCard} from "../../models/usercard.model";
import {UserCardService} from "../../../services/usercard/usercard.service";

@Component({
  selector: 'app-admin-office',
  templateUrl: './admin_office.component.html',
  styleUrls: ['./admin_office.component.css']
})
export class AdminOfficeComponent implements OnInit {
   products: Product[] = [];
   usercards: UserCard[] = [];

  constructor(private authService: AuthService, private router: Router,
              private catalogService: CatalogService,
              private udService: UserCardService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getUserCards();
  }
  create_movie(): void {
    this.router.navigate(['/movie-create']);
  }

  create_usercard(): void {
    this.router.navigate(['/usercard-create']);
  }

  getProducts(): void {
    this.catalogService.getProducts().subscribe(
      products => this.products = products,
      error => console.error('Error fetching movies:', error)
    );
  }

  getUserCards(): void {
    this.udService.getAllUserCards().subscribe(
      usercards => this.usercards = usercards,
      error => console.error('Error fetching usercards:', error)
    );

  }

  delete(id: string): void {
    this.catalogService.deleteProduct(id).subscribe(
      () => this.getProducts(),
      error => console.error('Error deleting movie:', error)
    );
  }
}
