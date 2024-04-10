import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import {CatalogService} from "./catalog/catalog.service";
import {CatalogComponent} from "./catalog/catalog.component";
import {FormsModule} from "@angular/forms";
import {HeaderComponent} from "./core/header/header.component";
import {HomeComponent} from "./home/home.component";
import {LoginComponent} from "./login/login.component";
import {BasketComponent} from "./basket/basket.component";
import {BasketService} from "./basket/basket.service";

@NgModule({
  declarations: [
    AppComponent,
    CatalogComponent,
    HeaderComponent,
    HomeComponent,
    LoginComponent,
    BasketComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [CatalogService, BasketService],
  bootstrap: [AppComponent]
})
export class AppModule { }
