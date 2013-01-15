using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using EFTest.Models.Mapping;

namespace EFTest.Models
{
    public class SentinelContext : DbContext
    {
        static SentinelContext()
        {
            Database.SetInitializer<SentinelContext>(null);
        }

		public SentinelContext()
            : base("Name=SentinelContext")
		{
		}

        public DbSet<EXPIRED_SESSION> EXPIRED_SESSION { get; set; }
        public DbSet<SESSION> SESSION { get; set; }
        public DbSet<LATITUDE_LONGITUDE> LATITUDE_LONGITUDE { get; set; }
        public DbSet<GEOGRAPHICAL_INFORMATION> GEOGRAPHICAL_INFORMATION { get; set; }
        public DbSet<CULTURE> CULTURE { get; set; }
        public DbSet<USER> USER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EXPIRED_SESSIONMap());
            modelBuilder.Configurations.Add(new SESSIONMap());
            modelBuilder.Configurations.Add(new LATITUDE_LONGITUDEMap());
            modelBuilder.Configurations.Add(new GEOGRAPHICAL_INFORMATIONMap());
            modelBuilder.Configurations.Add(new CULTUREMap());
            modelBuilder.Configurations.Add(new USERMap());
        }
    }
}
