export interface UserCard {
  userName: string; // змінено з username на userName
  bonuses: number;
  firstName: string; // змінено з firstname на firstName
  lastName: string;  // змінено з lastname на lastName
  emailAddress: string;
  addressLine?: string;
  country: string;
  state: string;
  zipCode?: string;
}

