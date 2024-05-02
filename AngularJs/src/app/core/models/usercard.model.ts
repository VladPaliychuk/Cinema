export interface UserCard {
  id: string;
  username: string;
  bonuses: number;

  firstname: string;
  lastname: string;
  email: string;
  address?: string;
  country: string;
  state: string;
  zipcode?: string;
}
