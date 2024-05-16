import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {AuthService} from "../../authService/auth.service";
import {CatalogService} from "../catalog.service";
import {MovieScreening} from "../../../core/models/movie-screening.model";

@Component({
  selector: 'screening-catalog',
  templateUrl: './screening-catalog.component.html',
  styleUrl: './screening-catalog.component.css'
})
export class ScreeningCatalogComponent implements OnInit{
  protected movieScreenings: MovieScreening[] = [];

  constructor(private catalogService: CatalogService, private auth: AuthService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.catalogService.getSortedScreeningsAndMoviesByDateTime().subscribe(movieScreenings => {
      this.movieScreenings = movieScreenings;
    });
  }
}
