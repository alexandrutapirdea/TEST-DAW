using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TestLaborator.Models
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Carte> ListaCarti { get; set; }
        public DbSet<Librarie> ListaLibrarii { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}