using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "CreatedAt", "Name", "UpdateAt", "Version" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "CHF - Swiss Franc", null, null },
                    { 2, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "EUR - Euro", null, null },
                    { 3, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "USD - U.S. dollar", null, null }
                });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "TransactionCategoryId", "CreatedAt", "Name", "UpdatedAt", "Version" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Food & Groceries", null, null },
                    { 2, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Utilities & Bills", null, null },
                    { 3, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Transportation", null, null },
                    { 4, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Medical/Healthcare", null, null },
                    { 5, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Houshold item/supplies", null, null },
                    { 6, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Personal", null, null },
                    { 7, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Gifts/donations", null, null },
                    { 8, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Entertainment", null, null },
                    { 9, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Retirement", null, null },
                    { 10, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Insurance", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumns: new[] { "CurrencyId", "Version" },
                keyValues: new object[] { 1, null });

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumns: new[] { "CurrencyId", "Version" },
                keyValues: new object[] { 2, null });

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumns: new[] { "CurrencyId", "Version" },
                keyValues: new object[] { 3, null });

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumns: new[] { "TransactionCategoryId", "Version" },
                keyValues: new object[] { 1, null });

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumns: new[] { "TransactionCategoryId", "Version" },
                keyValues: new object[] { 2, null });

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumns: new[] { "TransactionCategoryId", "Version" },
                keyValues: new object[] { 3, null });

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumns: new[] { "TransactionCategoryId", "Version" },
                keyValues: new object[] { 4, null });

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumns: new[] { "TransactionCategoryId", "Version" },
                keyValues: new object[] { 5, null });

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumns: new[] { "TransactionCategoryId", "Version" },
                keyValues: new object[] { 6, null });

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumns: new[] { "TransactionCategoryId", "Version" },
                keyValues: new object[] { 7, null });

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumns: new[] { "TransactionCategoryId", "Version" },
                keyValues: new object[] { 8, null });

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumns: new[] { "TransactionCategoryId", "Version" },
                keyValues: new object[] { 9, null });

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumns: new[] { "TransactionCategoryId", "Version" },
                keyValues: new object[] { 10, null });
        }
    }
}
