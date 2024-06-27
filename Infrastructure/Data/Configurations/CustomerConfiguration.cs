using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.Id);

        builder.Property(customer => customer.FirstName)
            .HasMaxLength(25);

        builder.Property(customer => customer.LastName)
            .HasMaxLength(25);

        builder.Property(customer => customer.Email)
            .HasMaxLength(30);
    }
}
