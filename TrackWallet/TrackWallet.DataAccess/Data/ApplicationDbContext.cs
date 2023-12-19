using Microsoft.EntityFrameworkCore;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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