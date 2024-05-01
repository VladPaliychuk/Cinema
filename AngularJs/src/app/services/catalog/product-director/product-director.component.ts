import {Component, OnInit} from '@angular/core';
import {Product} from "../../../core/models/product.model";
import {ActivatedRoute} from "@angular/router";
import {CatalogService} from "../catalog.service";

@Component({
  selector: 'app-product-director',
  templateUrl: './product-director.component.html',
  styleUrl: './product-director.component.css'
})
export class ProductDirectorComponent implements OnInit {
  products: Product[] = [];

  constructor(private route: ActivatedRoute, private catalogService: CatalogService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const director = decodeURIComponent(params.get('director') || '');
      this.catalogService.getProductsByDirector(director).subscribe(
        products => this.products = products,
        error => console.error('Error fetching movies:', error)
      );
    });
  }
}
