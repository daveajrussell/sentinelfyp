using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EFTest.Models.Mapping
{
    public class EXPIRED_SESSIONMap : EntityTypeConfiguration<EXPIRED_SESSION>
    {
        public EXPIRED_SESSIONMap()
        {
            // Primary Key
            this.HasKey(t => t.SESSION_ID);

            // Properties
            this.Property(t => t.SESSION_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.USER_AGENT)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.IP_ADDRESS)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EXPIRY_REASON)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EXPIRED_SESSION", "AUDIT");
            this.Property(t => t.SESSION_ID).HasColumnName("SESSION_ID");
            this.Property(t => t.USER_KEY).HasColumnName("USER_KEY");
            this.Property(t => t.USER_AGENT).HasColumnName("USER_AGENT");
            this.Property(t => t.IP_ADDRESS).HasColumnName("IP_ADDRESS");
            this.Property(t => t.SESSION_BEGIN_DATE_TIME).HasColumnName("SESSION_BEGIN_DATE_TIME");
            this.Property(t => t.EXPIRY_REASON).HasColumnName("EXPIRY_REASON");
        }
    }
}
