import { Portfolio } from "./portfolio";

export interface User {
    id: number;
    name: string;
    email: string;
    portfolios?: Portfolio[]
  }