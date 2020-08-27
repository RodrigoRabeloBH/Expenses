import { Component, OnInit, TemplateRef } from '@angular/core';
import { ExpenseService } from '../services/Expense.service';
import { Expense } from '../Models/Expense';
import { User } from '../Models/User';
import { Router } from '@angular/router';
import { AlertifyService } from '../services/Alertify.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';
pdfMake.vfs = pdfFonts.pdfMake.vfs;



@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {
  private index: number = 1;
  private length: number = 10;
  public expenses: Expense[];
  public interval: any = {};
  public total: number = 0;
  public data: number;
  public totalPage: number;
  public month: Date = new Date();
  public monthName: string;
  currentPage: number;
  modalRef: BsModalRef;

  constructor(private services: ExpenseService,
    private route: Router,
    private modalService: BsModalService,
    private alertifyServices: AlertifyService) { }

  ngOnInit() {

    this.getExpensesByInterval();
  }

  exportExcel(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);

  }
  generatePdf() {
    const documentDefinition = {
      content: [
        {
          text: 'RESUME',
          bold: true,
          fontSize: 20,
          alignment: 'center',
          margin: [0, 0, 0, 20],

          table: {
            // headers are automatically repeated if the table spans over multiple pages
            // you can declare how many rows should be treated as headers
            headerRows: 1,
            widths: ['*', 'auto', 100, '*'],
            body: [
              ['First', 'Second', 'Third', 'The last one'],
              ['Value 1', 'Value 2', 'Value 3', 'Value 4'],
              [{ text: 'Bold value', bold: true }, 'Val 2', 'Val 3', 'Val 4']
            ]
          }
        },
        {
          columns: [
            [{
              text: 'this.resume.name',
              style: 'name'
            },
            {
              text: this.interval.mindate
            },
            {
              text: this.interval.maxdate
            },
            {
              text: 'Contant No : ' + 'this.resume.contactNo',
            }
            ],
            [
              // Document definition for Profile pic
            ]
          ]
        }],
      styles: {
        name: {
          fontSize: 16,
          bold: true
        }
      }
    };


    pdfMake.createPdf(documentDefinition).open();
    this.modalRef.hide();
    this.interval = {};
  }

  exportPdf(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  getExpensesByUserId() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.services.GetExpensesByUserId(user.id, this.index, this.length)
      .subscribe(res => {
        this.expenses = res['data'];
      })
  }

  month_name(dt: Date) {

    const mlist = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    if (this.interval.mindate == null) {
      return mlist[dt.getMonth()];
    } else {
      const index = this.interval.mindate.slice(5, 7);
      return mlist[index - 1];
    }
  };

  getExpensesByInterval() {

    const user: User = JSON.parse(localStorage.getItem('user'));
    this.services.GetExpensesByInterval(this.interval, this.index, this.length, user.id)
      .subscribe(res => {
        this.expenses = res['data'];
        this.data = res['total'];
        this.totalPage = res['totalPage'] * 10;
      });
    this.getExpensesByMonth();

    this.monthName = this.interval.mindate != null ? this.month_name(new Date(this.interval.mindate)) : this.month_name(new Date());
  }
  getExpensesByMonth() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.total = 0;
    this.services.GetExpensesByInterval(this.interval, 1, 200, user.id)
      .subscribe(res => {
        let ex: any[];
        ex = res['data'];
        ex.forEach(e => {
          this.total += e.value;
        });
      });
  }

  deleteExpense(id: number) {
    this.services.DeleteExpense(id).subscribe(res => {
      this.alertifyServices.success("Expense removed successfully!");
      window.location.reload();
    }, error => {
      this.alertifyServices.error(error);
    })
  }
  navigate() {
    this.route.navigate(['create-expense']);
  }
  getExpenseById(id: number) {
    this.services.GetExpenseById(id).subscribe(res => {
      localStorage.setItem('expense', JSON.stringify(res));
      this.route.navigate(['update-expense']);
    })
  }

  pageChanged(event: any): void {
    this.index = event.page;
    this.total = 0;
    this.getExpensesByInterval();
  }
  refresh() {
    window.location.reload();
  }
}
