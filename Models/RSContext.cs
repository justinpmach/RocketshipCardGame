using Microsoft.EntityFrameworkCore;

namespace RocketShip.Models
{
    public class RSContext : DbContext
    {
        public RSContext(DbContextOptions options) : base(options) { }
        public DbSet<Card> Cards { get;set; }
        public DbSet<Player> Players {get; set;}
        public DbSet<Deck> Decks {get; set;}
    }
}
