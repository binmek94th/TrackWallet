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
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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