import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CatalogComponent} from "./services/catalog/catalog.component";
import {AuthGuard} from "./services/authService/auth.guard";
import {HomeComponent} from "./core/home/home.component";
import {LoginComponent} from "./core/login/login.component";
import {MovieCreateComponent} from "./services/movie/movie-create/movie-create.component";
import {AdminOfficeComponent} from "./core/personal_office/admin/admin-office.component";
import {MovieDetailsComponent} from "./services/movie/movie.component";
import {ProductActorComponent} from "./services/catalog/product-actor/product-actor.component";
import {ProductDirectorComponent} from "./services/catalog/product-director/product-director.component";
import {ProductGenreComponent} from "./services/catalog/product-genre/product-genre.component";
import {RegistrationComponent} from "./core/register/registration/registration.component";
import {EditMovieComponent} from "./services/movie/edit-movie/edit-movie.component";
import {ReservationComponent} from "./core/reservation/reservation.component";
import {ScreeningCatalogComponent} from "./services/catalog/screening-catalog/screening-catalog.component";
import {UserOfficeComponent} from "./core/personal_office/user/user-office/user-office.component";
import {
  UpdatePasswordComponent
} from "./core/personal_office/user/user-office/update-password/update-password.component";

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'catalog', component: CatalogComponent, canActivate: [AuthGuard]},
  { path: 'screening-catalog', component: ScreeningCatalogComponent, canActivate: [AuthGuard]},
  { path: 'movie-create', component: MovieCreateComponent, canActivate: [AuthGuard] },
  { path: 'movie-edit/:productName', component: EditMovieComponent, canActivate: [AuthGuard] },
  { path: 'admin-office', component: AdminOfficeComponent, canActivate: [AuthGuard] },
  { path: 'user-office', component:UserOfficeComponent, canActivate: [AuthGuard] },
  { path: 'movie/:productName', component: MovieDetailsComponent, canActivate: [AuthGuard] },
  { path: 'movies-by-actor/:actor', component: ProductActorComponent, canActivate: [AuthGuard] },
  { path: 'movies-by-director/:director', component: ProductDirectorComponent, canActivate: [AuthGuard] },
  { path: 'movies-by-genre/:genre', component: ProductGenreComponent, canActivate: [AuthGuard] },
  { path: 'reservation/:id', component: ReservationComponent, canActivate: [AuthGuard] },
  { path: 'update-password', component: UpdatePasswordComponent, canActivate: [AuthGuard] }
];

@NgModule({
  //imports: [RouterModule.forRoot(routes)],
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
