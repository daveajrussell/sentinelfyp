using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace EFTest.Models.Mapping
{
    public class SESSIONMap : EntityTypeConfiguration<SESSION>
    {
        public SESSIONMap()
        {
            // Primary Key
            this.HasKey(t => t.SESSION_ID);

            // Properties
            this.Property(t => t.USER_AGENT)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.IP_ADDRESS)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SESSION", "AUDIT");
            this.Property(t => t.SESSION_ID).HasColumnName("SESSION_ID");
            this.Property(t => t.USER_KEY).HasColumnName("USER_KEY");
            this.Property(t => t.SESSION_BEGIN_DATE_TIME).HasColumnName("SESSION_BEGIN_DATE_TIME");
            this.Property(t => t.USER_AGENT).HasColumnName("USER_AGENT");
            this.Property(t => t.IP_ADDRESS).HasColumnName("IP_ADDRESS");

            // Relationships
            this.HasRequired(t => t.USER)
                .WithMany(t => t.SESSIONs)
                .HasForeignKey(d => d.USER_KEY);

        }
    }
}
