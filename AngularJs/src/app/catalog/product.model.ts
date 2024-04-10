// src/app/catalog/product.model.ts

export interface Product {
  id: string;
  name: string;
  category: string;
  summary?: string;
  description?: string;
  imageFile?: string;
  price: number;
}
