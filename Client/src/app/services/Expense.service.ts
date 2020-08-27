import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Expense } from '../Models/Expense';
import { Observable } from 'rxjs';
import { ExpenseInterval } from '../Models/ExpenseInterval';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {

  private url: string = environment.url;

  constructor(private http: HttpClient) { }

  InsertExpense(expense: Expense): Observable<Expense> {

    return this.http.post<Expense>(this.url + 'expense/insert', expense);
  }

  GetAllExpenses(index: number, length: number): Observable<Expense[]> {

    return this.http.get<Expense[]>(this.url + 'expense/' + index + '/' + length);
  }

  GetExpensesByUserId(id: number, index: number, length: number): Observable<Expense[]> {

    return this.http.get<Expense[]>(this.url + 'expense/' + id + '/' + index + '/' + length);
  }

  GetExpenseById(id: number): Observable<Expense> {

    return this.http.get<Expense>(this.url + 'expense/' + id);
  }
  GetExpensesByInterval(interval: ExpenseInterval, index: number, length: number, id: number): Observable<Expense[]> {



    return this.http.post<Expense[]>(this.url + 'expense/' + id + '/' + index + '/' + length, interval);
  }

  DeleteExpense(id: number) {

    return this.http.delete(this.url + 'expense/' + id);
  }
  UpdateExpense(expense: Expense) {

    return this.http.put(this.url + 'expense', expense);
  }
}
