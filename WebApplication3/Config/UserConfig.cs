using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entity;

namespace WebApplication3.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).UseIdentityColumn();
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique(); 




        }
    }
}
