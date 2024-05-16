import {Screening} from "./screening.model";
import {Product} from "./product.model";

export interface MovieScreening {
  product: Product;
  screening: Screening;
}
