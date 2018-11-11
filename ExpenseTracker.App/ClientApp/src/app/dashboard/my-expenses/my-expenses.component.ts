import { Component, OnInit } from '@angular/core';
import { CreateTransaction } from '../models/createtransaction.interface';
import { UserTransactionsService } from '../services/usertransactions.service';

@Component({
  selector: 'app-my-expenses',
  templateUrl: './my-expenses.component.html',
  styleUrls: ['./my-expenses.component.scss']
})
export class MyExpensesComponent implements OnInit {

  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;

  constructor(private userTransactionsService: UserTransactionsService) { }

  ngOnInit() {
  }

  addExpense({ value, valid }: { value: CreateTransaction, valid: boolean }) {
    if (!valid) return;

    this.isRequesting = true;
    this.userTransactionsService.createUserTransaction(value)
      .finally(() => this.isRequesting = false)
      .subscribe(result => {
        console.log(result);
      },
      errors => this.errors = errors);
  }
  // registerUser({ value, valid }: { value: UserRegistration, valid: boolean }) {
  //   this.submitted = true;
  //   this.isRequesting = true;
  //   this.errors = '';
  //   if (valid) {
  //     this.userService.register(value)
  //       .finally(() => this.isRequesting = false)
  //       .subscribe(
  //         result => {
  //           if (result) {
  //             this.router.navigate(['/login'], { queryParams: { brandNew: true, email: value.email } });
  //           }
  //         },
  //         errors => this.errors = errors);
  //   }
  // }
}
