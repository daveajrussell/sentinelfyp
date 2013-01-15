using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace EFTest.Models.Mapping
{
    public class USERMap : EntityTypeConfiguration<USER>
    {
        public USERMap()
        {
            // Primary Key
            this.HasKey(t => t.USER_KEY);

            // Properties
            this.Property(t => t.USERNAME)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.SALT)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.HASH)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.FIRSTNAME)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.LASTNAME)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.EMAIL)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("USER", "SECURITY");
            this.Property(t => t.USER_KEY).HasColumnName("USER_KEY");
            this.Property(t => t.USER_CULTURE_KEY).HasColumnName("USER_CULTURE_KEY");
            this.Property(t => t.USERNAME).HasColumnName("USERNAME");
            this.Property(t => t.SALT).HasColumnName("SALT");
            this.Property(t => t.HASH).HasColumnName("HASH");
            this.Property(t => t.FIRSTNAME).HasColumnName("FIRSTNAME");
            this.Property(t => t.LASTNAME).HasColumnName("LASTNAME");
            this.Property(t => t.EMAIL).HasColumnName("EMAIL");
            this.Property(t => t.USER_ACCOUNT_CREATED_ON_DATE_TIME).HasColumnName("USER_ACCOUNT_CREATED_ON_DATE_TIME");
            this.Property(t => t.USER_LAST_LOGON_DATE_TIME).HasColumnName("USER_LAST_LOGON_DATE_TIME");
            this.Property(t => t.USER_ACCOUNT_EXPIRES_DATE_TIME).HasColumnName("USER_ACCOUNT_EXPIRES_DATE_TIME");

            // Relationships
            this.HasRequired(t => t.CULTURE)
                .WithMany(t => t.USERs)
                .HasForeignKey(d => d.USER_CULTURE_KEY);

        }
    }
}
