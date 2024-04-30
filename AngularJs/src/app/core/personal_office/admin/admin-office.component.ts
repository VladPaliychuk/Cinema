import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../../services/authService/auth.service";
import {Router} from "@angular/router";
import {CatalogService} from "../../../services/catalog/catalog.service";
import {Product} from "../../models/product.model";

@Component({
  selector: 'app-admin-office',
  templateUrl: './admin_office.component.html',
  styleUrls: ['./admin_office.component.css']
})
export class AdminOfficeComponent implements OnInit {
   products: Product[] = [];
  constructor(private authService: AuthService, private router: Router,
              private catalogService: CatalogService) { }

  ngOnInit(): void {
    this.getProducts();
  }
  create(): void {
    this.router.navigate(['/movie-create']);
  }
  getProducts(): void {
    this.catalogService.getProducts().subscribe(
      products => this.products = products,
      error => console.error('Error fetching movies:', error)
    );
  }

  delete(id: string): void {
    this.catalogService.deleteProduct(id).subscribe(
      () => this.getProducts(),
      error => console.error('Error deleting movie:', error)
    );
  }
}
