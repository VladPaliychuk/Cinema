import { Component, OnDestroy, OnInit } from '@angular/core';
import { CatalogService } from './catalog.service';
import { Product } from "../../core/models/product.model";
import { NavigationEnd, Router } from "@angular/router";
import { debounceTime, distinctUntilChanged, filter, Subject, switchMap, takeUntil } from "rxjs";
import { FormControl } from "@angular/forms";

@Component({
  selector: 'app-catalog',
  styleUrls: ['./catalog.component.css'],
  templateUrl: './catalog.component.html'
})
export class CatalogComponent implements OnInit, OnDestroy {
  products: Product[] = [];
  searchControl = new FormControl();
  page: number = 1;
  pageSize: number = 9;
  private unsubscribe$ = new Subject<void>();

  constructor(private catalogService: CatalogService, private router: Router) {
  }

  ngOnInit(): void {
    // Підписка на зміни значення пошукового терміну
    this.searchControl.valueChanges.pipe(
      debounceTime(300), // Затримка перед відправленням запиту
      distinctUntilChanged(), // Відправити запит лише якщо значення відрізняється від попереднього
      takeUntil(this.unsubscribe$) // Відписатися від підписки при знищенні компонента
    ).subscribe(searchTerm => {
      this.searchProducts(searchTerm); // Виклик методу пошуку при зміні значення інпута
    });

    // Отримання першої сторінки продуктів
    this.getProductsPage();
  }

  ngOnDestroy(): void {
    // При знищенні компонента відписатися від всіх підписок
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  // Метод для отримання першої сторінки продуктів
  getProductsPage(): void {
    this.catalogService.getProductsPage(this.page, this.pageSize).subscribe(
      products => this.products = products,
      error => console.error('Error fetching products:', error)
    );
  }

  // Метод для виконання пошуку продуктів
  searchProducts(searchTerm: string): void {
    if (!searchTerm.trim()) {
      // Якщо пошуковий термін порожній, відобразити всі продукти
      this.getProductsPage();
      return;
    }

    // Викликаємо метод для пошуку продуктів за введеним терміном
    this.catalogService.searchProductsLocally(searchTerm).subscribe(
      products => this.products = products,
      error => console.error('Error searching products:', error)
    );
  }

  // Методи для переміщення по сторінках
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
}
