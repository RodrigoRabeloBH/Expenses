import { Component, OnInit } from '@angular/core';
import { UserServicesService } from '../services/UserServices.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../services/Alertify.service';
import { Expense } from '../Models/Expense';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ExpenseService } from '../services/Expense.service';

@Component({
  selector: 'app-update-expense',
  templateUrl: './update-expense.component.html',
  styleUrls: ['./update-expense.component.css']
})
export class UpdateExpenseComponent implements OnInit {
  private expense: Expense;
  public updateForm: FormGroup;

  constructor(private services: ExpenseService,
    private router: Router,
    private alertifyServices: AlertifyService,
    private fb: FormBuilder) { }

  ngOnInit() {
    this.createUpdateForm();
    this.getEXpense();
  }

  createUpdateForm() {

    this.updateForm = this.fb.group({
      id: [''],
      name: ['', [Validators.required, Validators.maxLength(24)]],
      description: ['', [Validators.required, Validators.maxLength(200)]],
      value: ['', Validators.required],
      dueDate: ['', Validators.required],
      userId: ['']
    });
  }

  update() {

    if (this.updateForm.valid) {
      this.expense = Object.assign({}, this.updateForm.value);
      this.services.UpdateExpense(this.expense).subscribe(res => {
        this.alertifyServices.success("Updated successfully!");
        this.router.navigate(['expenses']);
      }, error => {
        this.alertifyServices.error(error);
      });
    }
  }
  getEXpense() {
    this.expense = JSON.parse(localStorage.getItem('expense'));
    this.updateForm.patchValue(this.expense);
  }
  navigate() {
    this.router.navigate(['expenses']);
  }

  replaceComma() {
    const input = document.querySelector('#value');
    input.addEventListener("input", (e) => {
      const target = e.target as HTMLInputElement;
      target.value = target.value
        .replace(',', '.');
    });
  }
}
