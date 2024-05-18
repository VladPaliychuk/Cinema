import { Product } from './product.model';
import { Genre } from './genre.model';
import { Actor } from './actor.model';
import { Screening } from './screening.model';
import {Director} from "./director.model";
import {ScreeningEdit} from "./screening-edit.model";

export interface Movie {
  product: Product;
  genres: Genre[];
  directors: Director[];
  actors: Actor[];
  screenings: Screening[];
}
