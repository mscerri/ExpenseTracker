using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExpenseTracker.Api.Data
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(m =>
            {
                m.HasIndex(u => u.UserGuid)
                    .IsUnique();

                m.HasIndex(u => u.Email)
                    .IsUnique();
            });

            modelBuilder.Entity<Transaction>(m =>
            {
                m.HasIndex(t => t.TransactionGuid)
                    .IsUnique();
            });

            modelBuilder.Entity<Currency>().HasData(
                new Currency { CurrencyId = 1, Name = "CHF - Swiss Franc" },
                new Currency { CurrencyId = 2, Name = "EUR - Euro" },
                new Currency { CurrencyId = 3, Name = "USD - U.S. dollar" });

            modelBuilder.Entity<TransactionCategory>().HasData(
                new TransactionCategory { TransactionCategoryId = 1, Name = "Food & Groceries" },
                new TransactionCategory { TransactionCategoryId = 2, Name = "Utilities & Bills" },
                new TransactionCategory { TransactionCategoryId = 3, Name = "Transportation" },
                new TransactionCategory { TransactionCategoryId = 4, Name = "Medical/Healthcare" },
                new TransactionCategory { TransactionCategoryId = 5, Name = "Houshold item/supplies" },
                new TransactionCategory { TransactionCategoryId = 6, Name = "Personal" },
                new TransactionCategory { TransactionCategoryId = 7, Name = "Gifts/donations" },
                new TransactionCategory { TransactionCategoryId = 8, Name = "Entertainment" },
                new TransactionCategory { TransactionCategoryId = 9, Name = "Retirement" },
                new TransactionCategory { TransactionCategoryId = 10, Name = "Insurance" });
        }
    }
}
