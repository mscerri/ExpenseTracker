import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';

import { Routing } from './account.routing';
import { LoginFormComponent } from './login-form/login-form.component';
import { RegistrationFormComponent } from './registration-form/registration-form.component';

import { UserService } from '../shared/services/user.service';

import { EmailValidator } from '../directives/email.validator.directive';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    Routing
  ],
  declarations: [
    LoginFormComponent,
    RegistrationFormComponent,
    EmailValidator
  ],
  providers: [
    UserService
  ]
})
export class AccountModule { }
