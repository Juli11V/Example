using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Entities;

namespace WebAPI.Infrastructure.Configurations;
public class TransferTransactionConfiguration : IEntityTypeConfiguration<TransferTransaction>
{
    public void Configure(EntityTypeBuilder<TransferTransaction> builder)
    {
        builder.Property(t => t.TransactionType).HasDefaultValue("Transfer");
        builder.HasOne(t => t.FromAccount)
               .WithMany(a => a.OutgoingTransactions)
               .HasForeignKey(t => t.FromAccountId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.Property(t => t.PaymentPurpose)
                   .HasMaxLength(100);
        builder.HasOne(t => t.ToAccount)
               .WithMany(a => a.IncomingTransferTransactions)
               .HasForeignKey(t => t.ToAccountId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

