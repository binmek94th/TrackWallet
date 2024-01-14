using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<UserSelectedCategory> UserSelectedCategories { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
    public DbSet<Occasion> Occasions { get; set; }
    public DbSet<LoanAndDebt> LoanAndDebts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<SharedWallet> SharedWallets { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Budget>()
            .HasOne(bar => bar.UserSelectedCategory)
            .WithMany(usc => usc.Budgets)
            .HasForeignKey(bar => bar.USCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Configure the relationship between UserSelectedCategory and BillAndReminder
        modelBuilder.Entity<UserSelectedCategory>()
            .HasMany(usc => usc.Budgets)
            .WithOne(bar => bar.UserSelectedCategory)
            .HasForeignKey(bar => bar.USCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Occasion>()
            .HasOne(bar => bar.Budget)
            .WithMany(usc => usc.Occasions)
            .HasForeignKey(bar => bar.BudgetId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Configure the relationship between UserSelectedCategory and BillAndReminder
        modelBuilder.Entity<Budget>()
            .HasMany(usc => usc.Occasions)
            .WithOne(bar => bar.Budget)
            .HasForeignKey(bar => bar.BudgetId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Goal>()
            .HasOne(g => g.Wallet)
            .WithMany(w => w.Goals)
            .HasForeignKey(g => g.WalletId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Wallet>()
            .HasMany(w => w.Goals)
            .WithOne(bar => bar.Wallet)
            .HasForeignKey(bar => bar.WalletId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<RecurringTransaction>()
            .HasOne(g => g.Wallet)
            .WithMany(w => w.RecurringTransactions)
            .HasForeignKey(g => g.WalletId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Wallet>()
            .HasMany(w => w.RecurringTransactions)
            .WithOne(bar => bar.Wallet)
            .HasForeignKey(bar => bar.WalletId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<RecurringTransaction>()
            .HasOne(bar => bar.UserSelectedCategory)
            .WithMany(usc => usc.RecurringTransactions)
            .HasForeignKey(bar => bar.USCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Configure the relationship between UserSelectedCategory and RecurringTransaction
        modelBuilder.Entity<UserSelectedCategory>()
            .HasMany(usc => usc.RecurringTransactions)
            .WithOne(bar => bar.UserSelectedCategory)
            .HasForeignKey(bar => bar.USCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Occasion>()
            .HasOne(bar => bar.Budget)
            .WithMany(usc => usc.Occasions)
            .HasForeignKey(bar => bar.BudgetId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Configure the relationship between UserSelectedCategory and RecurringTransaction
        modelBuilder.Entity<Budget>()
            .HasMany(usc => usc.Occasions)
            .WithOne(bar => bar.Budget)
            .HasForeignKey(bar => bar.BudgetId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<SharedWallet>()
            .HasOne(g => g.Wallet)
            .WithMany(w => w.SharedWallets)
            .HasForeignKey(g => g.WalletId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Wallet>()
            .HasMany(w => w.SharedWallets)
            .WithOne(bar => bar.Wallet)
            .HasForeignKey(bar => bar.WalletId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<SharedWallet>()
            .HasOne(g => g.Event)
            .WithMany(w => w.SharedWallets)
            .HasForeignKey(g => g.EventId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Event>()
            .HasMany(w => w.SharedWallets)
            .WithOne(bar => bar.Event)
            .HasForeignKey(bar => bar.EventId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
        modelBuilder.Entity<RecurringTransaction>()
            .HasOne(g => g.Event)
            .WithMany(w => w.RecurringTransactions)
            .HasForeignKey(g => g.EventId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Event>()
            .HasMany(w => w.RecurringTransactions)
            .WithOne(bar => bar.Event)
            .HasForeignKey(bar => bar.EventId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Budget>()
            .HasOne(g => g.Event)
            .WithMany(w => w.Budgets)
            .HasForeignKey(g => g.EventId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Event>()
            .HasMany(w => w.Budgets)
            .WithOne(bar => bar.Event)
            .HasForeignKey(bar => bar.EventId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<LoanAndDebt>()
            .HasOne(g => g.Event)
            .WithMany(w => w.LoanAndDebts)
            .HasForeignKey(g => g.EventId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Event>()
            .HasMany(w => w.LoanAndDebts)
            .WithOne(bar => bar.Event)
            .HasForeignKey(bar => bar.EventId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Notification>()
            .HasOne(g => g.Event)
            .WithMany(w => w.Notifications)
            .HasForeignKey(g => g.EventId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Event>()
            .HasMany(w => w.Notifications)
            .WithOne(bar => bar.Event)
            .HasForeignKey(bar => bar.EventId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Transaction>()
            .HasOne(g => g.Wallet)
            .WithMany(w => w.Transactions)
            .HasForeignKey(g => g.WalletId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Wallet>()
            .HasMany(w => w.Transactions)
            .WithOne(bar => bar.Wallet)
            .HasForeignKey(bar => bar.WalletId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Transaction>()
            .HasOne(g => g.LoanAndDebt)
            .WithMany(w => w.Transactions)
            .HasForeignKey(g => g.LoanAndDebtId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<LoanAndDebt>()
            .HasMany(w => w.Transactions)
            .WithOne(bar => bar.LoanAndDebt)
            .HasForeignKey(bar => bar.LoanAndDebtId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LoanAndDebt>()
            .HasOne(g => g.Wallet)
            .WithMany(w => w.LoanAndDebts)
            .HasForeignKey(g => g.WalletId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Wallet>()
            .HasMany(w => w.LoanAndDebts)
            .WithOne(bar => bar.Wallet)
            .HasForeignKey(bar => bar.WalletId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Transaction>()
            .HasOne(g => g.UserSelectedCategory)
            .WithMany(w => w.Transactions)
            .HasForeignKey(g => g.UserSelectedCategoryId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<UserSelectedCategory>()
            .HasMany(w => w.Transactions)
            .WithOne(bar => bar.UserSelectedCategory)
            .HasForeignKey(bar => bar.UserSelectedCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Transaction>()
            .HasOne(g => g.RecurringTransaction)
            .WithMany(w => w.Transactions)
            .HasForeignKey(g => g.RecurringTransactionId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<RecurringTransaction>()
            .HasMany(w => w.Transactions)
            .WithOne(bar => bar.RecurringTransaction)
            .HasForeignKey(bar => bar.RecurringTransactionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Food & Beverage",
                CategoryType = "Expense",
                ImageUrl = ""
            },
            new Category
            {
                Id = 2,
                Name = "Education",
                CategoryType = "Expense",
                ImageUrl = ""
            },
        new Category
            {
                Id = 3,
                Name = "Salary",
                CategoryType = "Income",
                ImageUrl = ""
            }
        );
    }
}