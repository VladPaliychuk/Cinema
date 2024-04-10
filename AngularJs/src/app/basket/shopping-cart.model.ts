// src/app/basket/shopping-cart.model.ts

export interface ShoppingCartItem {
  productId: string;
  productName: string;
  price: number;
  quantity: number;
  // Add any other properties you need
}

export interface ShoppingCart {
  userName: string;
  items: ShoppingCartItem[];
}
