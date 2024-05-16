import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import {CatalogService} from "./services/catalog/catalog.service";
import {CatalogComponent} from "./services/catalog/catalog.component";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HeaderComponent} from "./core/header/header.component";
import {HomeComponent} from "./core/home/home.component";
import {LoginComponent} from "./core/login/login.component";
import {MovieCreateComponent} from "./services/movie/movie-create/movie-create.component";
import {MovieService} from "./services/movie/movie.service";
import {AdminOfficeComponent} from "./core/personal_office/admin/admin-office.component";
import {MovieDetailsComponent} from "./services/movie/movie.component";
import {ProductActorComponent} from "./services/catalog/product-actor/product-actor.component";
import { ProductDirectorComponent } from './services/catalog/product-director/product-director.component';
import {ProductGenreComponent} from "./services/catalog/product-genre/product-genre.component";
import { RegistrationComponent } from './core/register/registration/registration.component';
import {UserService} from "./services/user/user.service";
import {RouterModule} from "@angular/router";
import {UserCardComponent} from "./services/usercard/usercard.component";
import { UserOfficeComponent } from './core/personal_office/user/user-office/user-office.component';
import { EditMovieComponent } from './services/movie/edit-movie/edit-movie.component';
import {UserCardService} from "./services/usercard/usercard.service";
import { ScreeningCatalogComponent } from './services/catalog/screening-catalog/screening-catalog.component';
import { ReservationComponent } from './core/reservation/reservation.component';
import { UpdatePasswordComponent } from './core/personal_office/user/user-office/update-password/update-password.component';

@NgModule({
  declarations: [
    AppComponent,
    CatalogComponent,
    HeaderComponent,
    HomeComponent,
    LoginComponent,
    MovieCreateComponent,
    AdminOfficeComponent,
    MovieDetailsComponent,
    ProductActorComponent,
    ProductDirectorComponent,
    ProductGenreComponent,
    RegistrationComponent,
    UserCardComponent,
    UserOfficeComponent,
    EditMovieComponent,
    ScreeningCatalogComponent,
    ReservationComponent,
    UpdatePasswordComponent
    //BasketComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
  ],
  providers: [CatalogService, MovieService, UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
