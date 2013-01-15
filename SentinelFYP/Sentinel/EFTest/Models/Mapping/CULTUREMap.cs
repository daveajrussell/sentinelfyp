using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace EFTest.Models.Mapping
{
    public class CULTUREMap : EntityTypeConfiguration<CULTURE>
    {
        public CULTUREMap()
        {
            // Primary Key
            this.HasKey(t => t.CULTURE_KEY);

            // Properties
            this.Property(t => t.REGION_CULTURE_CODE)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.LANGUAGE_CULTURE_CODE)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CULTURE", "SECURITY");
            this.Property(t => t.CULTURE_KEY).HasColumnName("CULTURE_KEY");
            this.Property(t => t.REGION_CULTURE_CODE).HasColumnName("REGION_CULTURE_CODE");
            this.Property(t => t.LANGUAGE_CULTURE_CODE).HasColumnName("LANGUAGE_CULTURE_CODE");
        }
    }
}
