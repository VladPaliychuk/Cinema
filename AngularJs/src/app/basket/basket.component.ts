// src/app/basket/basket.component.ts

import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket.service';
import { ShoppingCart } from './shopping-cart.model';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
<<<<<<< HEAD
  basket: ShoppingCart | undefined | null;
  userName: string = ''; // Replace with actual username
=======
  basket: ShoppingCart;
  userName: string = 'testUser'; // Replace with actual user name
>>>>>>> origin/master

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.getBasket();
  }

  getBasket(): void {
    this.basketService.getBasket(this.userName).subscribe(
      data => {
        this.basket = data;
      },
      error => {
        console.error('Error:', error);
      }
    );
  }

  updateBasket(): void {
    this.basketService.updateBasket(this.basket).subscribe(
      data => {
        this.basket = data;
      },
      error => {
        console.error('Error:', error);
      }
    );
  }

  deleteBasket(): void {
    this.basketService.deleteBasket(this.userName).subscribe(
      () => {
        this.basket = null;
      },
      error => {
        console.error('Error:', error);
      }
    );
  }
}
