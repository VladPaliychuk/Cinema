// src/app/catalog/product.model.ts

export interface Product {
  id: string;
  name: string;
  summary?: string;
  description?: string;
  imageFile?: string;
  releaseDate: string;
  duration: string;
  country: string;
  ageRestriction: string;
  price: number;
}

