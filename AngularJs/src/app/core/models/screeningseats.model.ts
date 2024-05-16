import {Screening} from "./screening.model";
import {Seat} from "./seat.model";

export interface ScreeningSeats {
  id: string;
  productId: string;
  startTime: string;
  startDate: string;
  seats: Seat[];
}
