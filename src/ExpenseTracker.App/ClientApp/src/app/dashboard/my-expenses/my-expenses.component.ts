import { Component, OnInit } from '@angular/core';
import { CreateTransaction } from '../models/createtransaction.interface';
import { UserTransactionsService } from '../services/usertransactions.service';
import { Transaction } from '../models/transaction.interface';
import { Observable } from 'rxjs/Observable';
import { Currency } from '../models/currency.interface';
import { TransactionCategory } from '../models/transactioncategory.interface';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-my-expenses',
  templateUrl: './my-expenses.component.html',
  styleUrls: ['./my-expenses.component.scss']
})
export class MyExpensesComponent implements OnInit {

  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;

  isLoading: boolean;

  public transactions$: Observable<Transaction[]>;
  public categories$: Observable<TransactionCategory[]>;
  public currencies$: Observable<Currency[]>;

  constructor(private userTransactionsService: UserTransactionsService) { }

  ngOnInit() {
    this.categories$ = this.userTransactionsService.getCategories();
    this.currencies$ = this.userTransactionsService.getCurrencies();

    this.loadExpenses();
  }

  loadExpenses() {
    this.isLoading = true;
    this.transactions$ = this.userTransactionsService.getCurrentUserTransactions().finally(() => this.isLoading = false);
  }

  addExpense(addExpenseForm: NgForm) {
    if (!addExpenseForm.valid) return;

    this.isRequesting = true;
    this.userTransactionsService.createUserTransaction(addExpenseForm.value)
      .finally(() => this.isRequesting = false)
      .subscribe(result => {
        this.errors = '';
        this.loadExpenses();     
        addExpenseForm.reset();
      }, errors => this.errors = errors);
  }

  removeExpense(transaction: Transaction) {
    this.userTransactionsService.deleteUserTransaction(transaction).subscribe(result => {
      this.loadExpenses();
    });
  }
}
