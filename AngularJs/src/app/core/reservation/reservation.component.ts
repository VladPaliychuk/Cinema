import {Component, OnInit} from '@angular/core';
import {CatalogService} from "../../services/catalog/catalog.service";
import {AuthService} from "../../services/authService/auth.service";
import {ActivatedRoute} from "@angular/router";
import {Screening} from "../models/screening.model";
import {ScreeningSeats} from "../models/screeningseats.model";
import {Product} from "../models/product.model";

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.css'
})
export class ReservationComponent implements OnInit{
  screeningSeats!: ScreeningSeats;
  movie!: Product;

  private username: string = '';
  constructor(private catalogService: CatalogService, private auth: AuthService,
              private route: ActivatedRoute) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.catalogService.getScreeningWithSeatsById(id).subscribe(screeningSeats => {
      this.screeningSeats = screeningSeats;
    });
    this.catalogService.getProductById(this.screeningSeats.screening.productId).subscribe(product => {
      this.movie = product;
    });
  }

  confirmReservation(seatId: string, screeningId: string): void {
    const isConfirmed = confirm('Ви впевнені, що хочете забронювати це місце?');
    if (isConfirmed) {
      // Викликати метод резервації, якщо користувач підтвердив резервацію
      this.catalogService.reserveSeat(screeningId, seatId, this.username).subscribe(
        (response: Blob) => {
          const url = window.URL.createObjectURL(response);
          const a = document.createElement('a');
          a.href = url;
          a.download = 'ReservationDetails.pdf';
          document.body.appendChild(a);
          a.click();
          window.URL.revokeObjectURL(url);
        },
        error => {
          // Обробити помилку резервації
          console.error('Error:', error);
        }
      );
    }
  }

  reserveSeat(screeningId: string, seatId: string, username: string): void {
    this.catalogService.reserveSeat(screeningId, seatId, username).subscribe(
      (response: Blob) => {
        const url = window.URL.createObjectURL(response);
        const a = document.createElement('a');
        a.href = url;
        a.download = 'ReservationDetails.pdf';
        document.body.appendChild(a);
        a.click();
        window.URL.revokeObjectURL(url);
      },
      error => {
        // Handle error
        console.error('Error:', error);
      }
    );
  }
}
