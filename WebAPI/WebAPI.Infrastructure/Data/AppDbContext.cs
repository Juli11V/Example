using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebAPI.Domain.Constants;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Configurations;

namespace WebAPI.Infrastructure.Data;
public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransferTransaction> TransferTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.ApplyConfigurationsFromAssembly(typeof(TransferTransactionConfiguration).Assembly);

        builder.Entity<Transaction>()
            .HasDiscriminator<string>("TransactionType")
            .HasValue<DepositTransaction>(TransactionTypes.Deposit)
            .HasValue<WithdrawTransaction>(TransactionTypes.Withdraw);
        builder.Entity<Account>()
            .HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId);


    }
}

