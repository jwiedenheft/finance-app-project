using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp;

public class FinanceAppDbContext : DbContext
{
    public FinanceAppDbContext(DbContextOptions<FinanceAppDbContext> options) : base(options)
    {

    }

    public DbSet<Transaction> Transactions { get; set; }

}