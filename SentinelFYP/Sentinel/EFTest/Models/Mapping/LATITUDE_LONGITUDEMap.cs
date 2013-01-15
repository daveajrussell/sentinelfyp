using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EFTest.Models.Mapping
{
    public class LATITUDE_LONGITUDEMap : EntityTypeConfiguration<LATITUDE_LONGITUDE>
    {
        public LATITUDE_LONGITUDEMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LATITUDE, t.LONGITUDE });

            // Properties
            this.Property(t => t.LATITUDE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LONGITUDE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("LATITUDE_LONGITUDE");
            this.Property(t => t.LATITUDE).HasColumnName("LATITUDE");
            this.Property(t => t.LONGITUDE).HasColumnName("LONGITUDE");
        }
    }
}
