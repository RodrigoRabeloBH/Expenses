import { Component, OnInit } from '@angular/core';
import { Expense } from '../Models/Expense';
import { ExpenseService } from '../services/Expense.service';
import { AlertifyService } from '../services/Alertify.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../Models/User';

@Component({
  selector: 'app-create-expense',
  templateUrl: './create-expense.component.html',
  styleUrls: ['./create-expense.component.css']
})
export class CreateExpenseComponent implements OnInit {


  public insertForm: FormGroup;
  private expense: Expense;

  constructor(private expenseServices: ExpenseService,
    private alertifyServices: AlertifyService,
    private fb: FormBuilder,
    private router: Router) { }

  ngOnInit() {
    this.createdInsetForm();
  }

  createdInsetForm() {

    const user: User = JSON.parse(localStorage.getItem('user'));

    this.insertForm = this.fb.group({
      userId: [user.id],
      value: ['', Validators.required],
      name: ['', [Validators.required, Validators.maxLength(24)]],
      description: ['', [Validators.required, Validators.maxLength(200)]],
      dueDate: ['', Validators.required],
    });
  }

  insertExpense() {
    if (this.insertForm.valid) {
      this.expense = Object.assign({}, this.insertForm.value);
      this.expenseServices.InsertExpense(this.expense)
        .subscribe(() => {
          this.alertifyServices.success("Resgistration successful!");
          this.router.navigate(['expenses']);
        }, error => {
          this.alertifyServices.error(error);
        });
    }
  }
  replaceComma() {
    const input = document.querySelector('#value');
    input.addEventListener("input", (e) => {
      const target = e.target as HTMLInputElement;
      target.value = target.value
        .replace(',', '.');
    });
  }
  cancel() {
    history.back();
  }
}
