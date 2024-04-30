import {Component, OnDestroy, OnInit} from '@angular/core';
import { CatalogService } from './catalog.service';
import {Product} from "../../core/models/product.model";
import {NavigationEnd, Router} from "@angular/router";
import {filter, Subject, takeUntil} from "rxjs";

@Component({
  selector: 'app-catalog',
  styleUrls: ['./catalog.component.css'],
  templateUrl: './catalog.component.html'
})
export class CatalogComponent implements OnInit, OnDestroy {
  products: Product[] = [];
  searchName: string = '';
  searchCategory: string = '';
  page: number = 1;
  pageSize: number = 9;
  private unsubscribe$ = new Subject<void>();

  constructor(private catalogService: CatalogService, private router: Router) {
    // Subscribe to the router events - filtering for NavigationEnd events
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      takeUntil(this.unsubscribe$)
    ).subscribe(event => {
      if (this.searchCategory) {
        this.getProductsPage(); // Call your method here
      } else {
        this.getProductsPage(); // Call your method here
      }
    });
  }

  //TODO: Налаштувати вигляд
  ngOnInit(): void {
    this.getProductsPage();
  }
  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
  getProductsPage(): void {
    this.catalogService.getProductsPage(this.page, this.pageSize).subscribe(
      products => this.products = products,
      error => console.error('Error fetching products:', error)
    );
  }
  nextPage(): void {
    this.page++;
    this.getProductsPage();
  }
  previousPage(): void {
    if (this.page > 1) {
      this.page--;
      this.getProductsPage();
    }
  }

  getProducts(): void {
    this.catalogService.getProducts().subscribe(
      products => this.products = products,
      error => console.error('Error fetching products:', error)
    );
  }
  searchByName(): void {
    if (this.searchName) {
      this.catalogService.getProductsByName(this.searchName).subscribe(
        products => this.products = products,
        error => console.error('Error fetching products by name:', error)
      );
    }
  }

  searchByCategory(): void {
    if (this.searchCategory) {
      this.catalogService.getProductsByCategory(this.searchCategory).subscribe(
        products => this.products = products,
        error => console.error('Error fetching products by category:', error)
      );
    } else {
      this.products = [];
    }
  }

  search(): void {
    if (this.searchName || this.searchCategory) {
      this.catalogService.searchProducts(this.searchName, this.searchCategory).subscribe(
        products => this.products = products,
        error => console.error('Error fetching products:', error)
      );
    }
  }
}
