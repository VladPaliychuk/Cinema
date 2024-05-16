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
    this.catalogService.getScreeningWithSeatsById(id).subscribe(
      screeningSeats => {
        console.log('Screening Seats:', screeningSeats);
        this.screeningSeats = screeningSeats;
        console.log('Screening Seats:', this.screeningSeats);
        if (this.screeningSeats) {
          this.catalogService.getProductById(this.screeningSeats.productId).subscribe(
            product => {
              this.movie = product;
              this.username = this.auth.getUsername();
              console.log('Product:', this.movie);
              console.log('Username:', this.username);
            },
            error => {
              console.error('Error fetching product:', error);
            }
          );
        }
      },
      error => {
        console.error('Error fetching screening seats:', error);
      }
    );
  }


  confirmReservation(seatId: string): void {
    const isConfirmed = confirm('Ви впевнені, що хочете забронювати це місце?');
    if (isConfirmed) {
      this.catalogService.reserveSeat(this.screeningSeats.id, seatId, this.username).subscribe(
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
          console.error('Error:', error);
        }
      );
    }
  }
}
