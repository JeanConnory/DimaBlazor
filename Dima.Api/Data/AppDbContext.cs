using Dima.Api.Models;
using Dima.Core.Models;
using Dima.Core.Models.Reports;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dima.Api.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<long>, long, 
        IdentityUserClaim<long>, IdentityUserRole<long>, IdentityUserLogin<long>,
        IdentityRoleClaim<long>, IdentityUserToken<long>>
{
    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Transaction> Transactions { get; set; } = null!;

    public DbSet<IncomesAndExpenses> IncomesAndExpenses { get; set; } = null!;
    public DbSet<IncomesByCategory> IncomesByCategories { get; set; } = null!;
    public DbSet<ExpensesByCategory> ExpensesByCategories { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<IncomesAndExpenses>()
            .HasNoKey()
            .ToView("vwGetIncomesAndExpenses");

        modelBuilder.Entity<IncomesByCategory>()
            .HasNoKey()
            .ToView("vwGetIncomesByCategory");

        modelBuilder.Entity<ExpensesByCategory>()
            .HasNoKey()
            .ToView("vwGetExpensesByCategory");
    }
}
