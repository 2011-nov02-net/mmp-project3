import { Portfolio } from "./portfolio";

export interface User {
    id: number;
    firstName: string,
    lastName: string,
    email: string;
    portfolios?: Portfolio[],
    currentPort: number
  }