import { Expense } from './Expense';

export interface User {
    id: number;
    firstname: string;
    lastname: string;
    phone: string;
    username: string;
    password: string;
    email: string;
    expenses: Expense[];
}