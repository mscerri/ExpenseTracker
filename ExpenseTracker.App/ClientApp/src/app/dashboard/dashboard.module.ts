import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }        from '@angular/forms';
import { SharedModule }       from '../shared/shared.module';

import { RootComponent } from './root/root.component';
import { MyExpensesComponent } from './my-expenses/my-expenses.component';
import { MyProfileComponent } from './my-profile/my-profile.component';

import { AuthGuard } from '../auth.guard';

import { Routing }  from './dashboard.routing';
import { UserTransactionsService } from './services/usertransactions.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    Routing
  ],
  declarations: [
    RootComponent, 
    MyExpensesComponent, 
    MyProfileComponent
  ],
  providers: [
    AuthGuard,
    UserTransactionsService
  ]
})
export class DashboardModule { }
