using CORM.EntityFrameworkCore.Storage;
using CORM.TestConsole.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CORM.TestConsole.Configurations;

public class OrderConfiguration : EntityTypeConfiguration<Order>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        
        builder.Property(o => o.TotalAmount)
            .HasPrecision(18, 2);
        
        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}