import {Component, OnInit} from '@angular/core';
import {Product} from "../../../core/models/product.model";
import {ActivatedRoute} from "@angular/router";
import {CatalogService} from "../catalog.service";

@Component({
  selector: 'app-product-genre',
  templateUrl: './product-genre.component.html',
  styleUrl: './product-genre.component.css'
})
export class ProductGenreComponent implements OnInit {
  products: Product[] = [];

  constructor(private route: ActivatedRoute, private catalogService: CatalogService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const genre = decodeURIComponent(params.get('genre') || '');
      this.catalogService.getProductsByGenre(genre).subscribe(
        products => this.products = products,
        error => console.error('Error fetching movies:', error)
      );
    });
  }
}
