using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BadanieKrwi.Data_Base
{
    public partial class AppDbContext : DbContext
    {
        private readonly string _connString = ConfigurationManager
                                            .ConnectionStrings["ConnectionString"].ConnectionString;

        public DbSet<Uzytkownik> Uzytkownik { get; set; }
        public DbSet<Badania> Badania { get; set; }
        public DbSet<Kliniki> Kliniki { get; set; }

        public AppDbContext()
        {
            Database.Migrate();
        }

        // uzycie SQL serwera i connectionString, który jest odczytywany z App.config
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connString);
        }
    }    
}