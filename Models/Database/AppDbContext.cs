using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BadanieKrwi.Models.Database
{
    public partial class AppDbContext : DbContext
    {
        private readonly string _connString = ConfigurationManager
                                            .ConnectionStrings["ConnectionString"].ConnectionString;

        public DbSet<Uzytkownik> Uzytkownik { get; set; }
        public DbSet<BadanieModel> Badania { get; set; }
        public DbSet<Klinika> Kliniki { get; set; }
        public DbSet<KalendarzBadanModel> Kalendarz { get; set; }

        // uzycie SQL serwera i connectionString, który jest odczytywany z App.config
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connString);
        }
    }    
}