import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { MovieService } from '../movie.service';
import { Movie } from '../../../core/models/movie.model';

@Component({
  selector: 'app-movie-create',
  templateUrl: './movie-create.component.html',
  styleUrls: ['./movie-create.component.css']
})
export class MovieCreateComponent implements OnInit {
  movieForm!: FormGroup;
  successMessage: string = '';

  constructor(private fb: FormBuilder, private movieService: MovieService) { }

  ngOnInit(): void {
    this.movieForm = this.fb.group({
      product: this.fb.group({
        name: ['', [Validators.required, Validators.minLength(3)]],
        summary: ['', [Validators.required, Validators.minLength(3)]],
        description: ['', [Validators.required, Validators.minLength(3)]],
        imageFile: ['', [Validators.required, Validators.minLength(3)]],
        releaseDate: ['', [Validators.required, Validators.minLength(3)]],
        duration: ['', [Validators.required, Validators.minLength(3)]],
        country: ['', [Validators.required, Validators.minLength(3)]],
        ageRestriction: ['', [Validators.required, Validators.minLength(3)]],
        price: [0, [Validators.required, Validators.min(70)]],
      }),
      genres: this.fb.array([]),
      actors: this.fb.array([]),
      directors: this.fb.array([]),
      screenings: this.fb.array([]),
    });
  }

  get genres(): FormArray {
    return this.movieForm.get('genres') as FormArray;
  }

  get actors(): FormArray {
    return this.movieForm.get('actors') as FormArray;
  }

  get directors(): FormArray {
    return this.movieForm.get('directors') as FormArray;
  }

  get screenings(): FormArray {
    return this.movieForm.get('screenings') as FormArray;
  }

  addGenre(): void {
    this.genres.push(this.fb.group({ name: ['', Validators.required] }));
  }

  addDirector(): void {
    this.directors.push(this.fb.group({ firstName: ['', Validators.required], lastName: ['', Validators.required] }));
  }

  addActor(): void {
    this.actors.push(this.fb.group({ firstName: ['', Validators.required], lastName: ['', Validators.required] }));
  }

  addScreening(): void {
    this.screenings.push(this.fb.group({ startTime: ['', Validators.required], startDate: ['', Validators.required] }));
  }

  createMovie(): void {
    if (this.movieForm.valid) {
      this.movieService.createMovie(this.movieForm.value).subscribe(
        response => {
          console.log('Movie created!', response);
          this.movieForm.reset(); // Reset the form
          this.successMessage = 'Movie is created successfully'; // Set the success message
        },
        error => console.error('Error creating movie', error)
      );
    }
  }
}
