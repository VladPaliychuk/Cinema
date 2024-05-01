import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {Movie} from "../../core/models/movie.model";
import {MovieService} from "./movie.service";
@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieDetailsComponent implements OnInit {
  movie!: Movie;

  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const productName = decodeURIComponent(<string>params.get('productName'));
      this.movieService.getMovie(productName).subscribe(
        movie => this.movie = movie,
        error => console.error('Error fetching movie:', error)
      );
    });
  }
}
