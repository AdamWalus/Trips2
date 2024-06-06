using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trips.Models;
namespace Trips.Data
{
    public class TripReservationDbContext : IdentityDbContext<IdentityUser>
    {
        public TripReservationDbContext(DbContextOptions<TripReservationDbContext> options) : base(options) { }

        public DbSet <Client> Client { get; set; }
        public DbSet <Trip> Trip { get; set; }
        public DbSet <Reservation> Reservation { get; set; }

    }
}
