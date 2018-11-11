import { TransactionCategory } from "./transactioncategory.interface";
import { Currency } from "./currency.interface";

export interface Transaction {
    id: string,
    amount: number
    currency: Currency,
    category: TransactionCategory,
    note: string,
    executionDate: Date
}