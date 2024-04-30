import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import {CatalogService} from "./services/catalog/catalog.service";
import {CatalogComponent} from "./services/catalog/catalog.component";
import {FormArray, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HeaderComponent} from "./core/header/header.component";
import {HomeComponent} from "./core/home/home.component";
import {LoginComponent} from "./core/login/login.component";
import {MovieCreateComponent} from "./services/movie/movie-create/movie-create.component";
import {MovieService} from "./services/movie/movie.service";
import {AdminOfficeComponent} from "./core/personal_office/admin/admin-office.component";
import {MovieDetailsComponent} from "./services/movie/movie.component";

@NgModule({
  declarations: [
    AppComponent,
    CatalogComponent,
    HeaderComponent,
    HomeComponent,
    LoginComponent,
    MovieCreateComponent,
    AdminOfficeComponent,
    MovieDetailsComponent
    //BasketComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [CatalogService, MovieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
