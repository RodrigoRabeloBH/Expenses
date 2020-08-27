import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from 'src/app/register/register.component';
import { GuardsGuard } from 'src/app/guards/auth.guard';
import { HomeComponent } from 'src/app/home/home.component';
import { ExpensesComponent } from 'src/app/expenses/expenses.component';
import { CreateExpenseComponent } from 'src/app/create-expense/create-expense.component';
import { UpdateExpenseComponent } from 'src/app/update-expense/update-expense.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'register', component: RegisterComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [GuardsGuard],
        children: [
            { path: 'expenses', component: ExpensesComponent },
            { path: 'create-expense', component: CreateExpenseComponent },
            { path: 'update-expense', component: UpdateExpenseComponent },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }

];

@NgModule({
    declarations: [],
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
