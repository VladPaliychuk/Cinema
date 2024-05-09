import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { Movie } from '../../../core/models/movie.model';
import {ActivatedRoute} from "@angular/router";
import {MovieService} from "../movie.service";
import {CatalogService} from "../../catalog/catalog.service";
import {FormArray, FormGroup} from "@angular/forms";
import {tap} from "rxjs";

@Component({
  selector: 'app-edit-movie',
  templateUrl: './edit-movie.component.html',
  styleUrl: './edit-movie.component.css'
})
export class EditMovieComponent implements OnInit{
  movieForm!: FormGroup;
  movie!: Movie;
  firstname!: string;
  lastname!: string;
  genre!: string;

  constructor(
    private route: ActivatedRoute,
    private changeDetector: ChangeDetectorRef,
    private movieService: MovieService,
    private catalogService: CatalogService
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

  fetchMovieData(): void {
    this.movieService.getMovie(this.movie.product.name).subscribe(
      movie => {
        this.movie = movie;
        this.fetchMovieData();
      },
      error => console.error('Error fetching movie:', error)
    );
  }

  deleteActor(actor: any): void {
    this.firstname = actor.firstName;
    this.lastname = actor.lastName;
    this.catalogService.deleteProductActorRelation(this.movie.product.name,
      this.firstname + ' ' + this.lastname).pipe(
      tap(() => {
        console.log('Actor deleted');
        const index = this.movie.actors.findIndex(a => a.firstName === this.firstname && a.lastName === this.lastname);
        if (index > -1) {
          this.movie.actors.splice(index, 1);
        }
      }),
      tap(() => this.fetchMovieData())
    ).subscribe(
      () => {},
      error => console.error('Error deleting actor:', error)
    );
  }

  deleteDirector(director: any): void {
    this.firstname = director.firstName;
    this.lastname = director.lastName;
    this.catalogService.deleteProductDirectorRelation(this.movie.product.name,
      this.firstname + ' ' + this.lastname).pipe(
      tap(() => {
        console.log('Director deleted');
        const index = this.movie.directors.findIndex(d => d.firstName === this.firstname && d.lastName === this.lastname);
        if (index > -1) {
          this.movie.directors.splice(index, 1);
        }
      }),
      tap(() => this.fetchMovieData())
    ).subscribe(
      () => {},
      error => console.error('Error deleting director:', error)
    );
  }

  deleteGenre(genre: any): void {
    this.genre = genre.name;
    this.catalogService.deleteProductGenreRelation(this.movie.product.name, this.genre).pipe(
      tap(() => {
        console.log('Genre deleted');
        const index = this.movie.genres.findIndex(g => g.name === this.genre);
        if (index > -1) {
          this.movie.genres.splice(index, 1);
        }
      }),
      tap(() => this.fetchMovieData())
    ).subscribe(
      () => {},
      error => console.error('Error deleting genre:', error)
    );
  }

  addActor(firstname: string, lastname: string): void {
    this.catalogService.createProductActorRelation(this.movie.product.name,
      firstname + ' ' + lastname).pipe(
      tap(() => {
        console.log('Actor added');
        this.movie.actors.push({firstName: firstname, lastName: lastname});
      }),
      tap(() => this.fetchMovieData())
    ).subscribe(
      () => {},
      error => console.error('Error adding actor:', error)
    );
  }

  addDirector(firstname: string, lastname: string): void {
    this.catalogService.createProductDirectorRelation(this.movie.product.name,
      firstname + ' ' + lastname).pipe(
      tap(() => {
        console.log('Director added');
        this.movie.directors.push({firstName: firstname, lastName: lastname});
      }),
      tap(() => this.fetchMovieData())
    ).subscribe(
      () => {},
      error => console.error('Error adding director:', error)
    );
  }

  addGenre(genre: string): void {
    this.catalogService.createProductGenreRelation(this.movie.product.name, genre).pipe(
      tap(() => {
        console.log('Genre added');
        this.movie.genres.push({name: genre});
      }),
      tap(() => this.fetchMovieData())
    ).subscribe(
      () => {
      },
      error => console.error('Error adding genre:', error)
    );
  }
}
