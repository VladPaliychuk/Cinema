import { Component, OnInit } from "@angular/core";
import { CatalogService } from "../catalog.service";
import { Product } from "../../../core/models/product.model";
import { ActivatedRoute } from "@angular/router";
import { Observable } from "rxjs";

@Component({
  selector: 'app-product-actor',
  templateUrl: './product-actor.component.html',
  styleUrls: ['./product-actor.component.css']
})
export class ProductActorComponent implements OnInit {
  products: Product[] = [];

  constructor(private route: ActivatedRoute, private catalogService: CatalogService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const actor = decodeURIComponent(params.get('actor') || '');
      this.catalogService.getProductsByActor(actor).subscribe(
        products => this.products = products,
        error => console.error('Error fetching movies:', error)
      );
    });
  }
}
