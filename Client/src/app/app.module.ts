import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { JwtModule } from "@auth0/angular-jwt";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ChartsModule } from 'ng2-charts';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RegisterComponent } from './register/register.component';
import { HttpClientModule } from '@angular/common/http';
import { UserServicesService } from './services/UserServices.service';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from 'routes';
import { ExpensesComponent } from './expenses/expenses.component';
import { CreateExpenseComponent } from './create-expense/create-expense.component';
import { FooterComponent } from './footer/footer.component';
import { UpdateExpenseComponent } from './update-expense/update-expense.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';

export function tokenGetter() {
  return localStorage.getItem('token');
}
export class CustomHammerConfig extends HammerGestureConfig {
  overrides = {
    pinch: { enable: false },
    rotate: { enable: false }
  };
}
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    RegisterComponent,
    HomeComponent,
    ExpensesComponent,
    CreateExpenseComponent,
    FooterComponent,
    UpdateExpenseComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ChartsModule,
    FormsModule,
    AppRoutingModule,
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['http://localhost:5000'],
        disallowedRoutes: ["http://localhost:5000/api/user"],
      },
    }),
    PaginationModule.forRoot(),
  ],
  providers: [
    UserServicesService,
    { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
