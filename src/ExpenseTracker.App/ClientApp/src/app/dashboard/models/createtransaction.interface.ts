export interface CreateTransaction {
    amount: number,
    currencyId: number,
    categoryId: number,
    note: string,
    executionDate: Date
}