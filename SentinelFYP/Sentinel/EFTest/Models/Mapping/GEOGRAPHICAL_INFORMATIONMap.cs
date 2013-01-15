using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EFTest.Models.Mapping
{
    public class GEOGRAPHICAL_INFORMATIONMap : EntityTypeConfiguration<GEOGRAPHICAL_INFORMATION>
    {
        public GEOGRAPHICAL_INFORMATIONMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UPDATE_DATE_TIME, t.LATITUDE, t.LONGITUDE, t.SPEED, t.ORIENTATION });

            // Properties
            this.Property(t => t.LATITUDE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LONGITUDE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SPEED)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ORIENTATION)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("GEOGRAPHICAL_INFORMATION", "GIS");
            this.Property(t => t.UPDATE_DATE_TIME).HasColumnName("UPDATE_DATE_TIME");
            this.Property(t => t.LATITUDE).HasColumnName("LATITUDE");
            this.Property(t => t.LONGITUDE).HasColumnName("LONGITUDE");
            this.Property(t => t.SPEED).HasColumnName("SPEED");
            this.Property(t => t.ORIENTATION).HasColumnName("ORIENTATION");
        }
    }
}
