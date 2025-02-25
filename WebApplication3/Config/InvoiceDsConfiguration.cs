using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entity.DataBase;

namespace WebApplication3.Config
{
    public class InvoiceDsConfiguration: IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {

            builder.HasKey(x => x.DetailId);
            builder.Property(x => x.DetailId).UseIdentityColumn();
            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(255);


            builder.HasOne(x => x.InvoiceHeader).WithMany(x => x.InvoiceDetails)
              .HasForeignKey(x => x.InvoiceHId)
              .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(x => x.Product).WithMany(x => x.InvoiceDetails)
              .HasForeignKey(x => x.ProductID)
              .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
