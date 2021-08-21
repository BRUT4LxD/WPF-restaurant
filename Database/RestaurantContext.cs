using Database.Model;
using Database.ModelConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantEntityConfiguration).Assembly);
        }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
