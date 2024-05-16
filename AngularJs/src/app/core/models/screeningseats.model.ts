import {Screening} from "./screening.model";
import {Seat} from "./seat.model";

export interface ScreeningSeats {
  screening: Screening,
  seats: Seat[],
}
