using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entity;

namespace WebApplication3.Config
{
    internal class InvoiceHeaderConfiguration : IEntityTypeConfiguration<InvoiceHeader>
    {
        public void Configure(EntityTypeBuilder<InvoiceHeader> builder)
        {

            builder.HasKey(x => x.InvoiceHId);
            builder.Property(x => x.InvoiceHId).UseIdentityColumn();
            builder.Property(x => x.InvoiceName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.TotalAmount).IsRequired();


            
        }
    }
}
