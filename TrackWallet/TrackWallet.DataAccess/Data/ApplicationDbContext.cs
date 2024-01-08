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
    public DbSet<BillAndReminder> BillAndReminders { get; set; }
    public DbSet<Goal> Goals { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BillAndReminder>()
            .HasOne(bar => bar.UserSelectedCategory)
            .WithMany(usc => usc.BillAndReminders)
            .HasForeignKey(bar => bar.USCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Configure the relationship between UserSelectedCategory and BillAndReminder
        modelBuilder.Entity<UserSelectedCategory>()
            .HasMany(usc => usc.BillAndReminders)
            .WithOne(bar => bar.UserSelectedCategory)
            .HasForeignKey(bar => bar.USCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

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