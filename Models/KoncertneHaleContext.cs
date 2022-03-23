using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class KoncertneHaleContext :DbContext
    {
        public KoncertneHaleContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Rezervacija> Rezervacije {get; set; }
        public DbSet<Koncert> Koncerti {get; set; }
        public DbSet<Izvodjac> Izvodjaci {get; set; }
        public DbSet<Karta> Karte {get; set; }
        public DbSet<Hala> Hale {get; set; }
    }
}