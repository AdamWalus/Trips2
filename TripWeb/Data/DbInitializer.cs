using Trips.Models;

namespace Trips.Data
{
    public class DbInitializer
    {
        public static void Initialize(TripReservationDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if trips exist in the database, if yes, do not seed them again
            if (context.Trip.Any()) return;

            var trips = new Trip[]
            {
                new Trip { Title = "Krakow", Description = "Explore the historic city of Krakow", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(4) },
                new Trip { Title = "Berlin", Description = "Experience the cultural hub of Berlin", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5) },
                new Trip { Title = "Rome", Description = "Discover the ancient city of Rome", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(6) },
                new Trip { Title = "Tokyo", Description = "Explore the bustling metropolis of Tokyo", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7) },
                new Trip { Title = "Vienna", Description = "Explore vienna because why not", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7) },


            };

            foreach (Trip t in trips)
            {
                context.Trip.Add(t);
            }

            context.SaveChanges();

            var clients = new Client[]
            {
                new Client { Name = "Adam", Surname = "Kowalski", Email = "adam.kowalski@example.com" },
                new Client { Name = "Ewa", Surname = "Nowak", Email = "ewa.nowak@example.com" },
                new Client { Name = "Tomasz", Surname = "Wiśniewski", Email = "tomasz.wisniewski@example.com" },
                new Client { Name = "Anna", Surname = "Dąbrowska", Email = "anna.dabrowska@example.com" },
                new Client { Name = "Andrzej", Surname = "Kolejwoski", Email = "anna.ddd@example.com" },
                new Client { Name = "Dominik", Surname = "Bobobo", Email = "anna.dabrodwska@example.com" },


            };

            foreach (Client c in clients)
            {
                context.Client.Add(c);
            }

            context.SaveChanges();

            var reservations = new Reservation[]
            {
                new Reservation {ReservationDate = DateTime.Now.AddDays(3),},
                new Reservation {ReservationDate = DateTime.Now.AddDays(4),},
                new Reservation {ReservationDate = DateTime.Now.AddDays(5), },
                new Reservation {ReservationDate = DateTime.Now.AddDays(6),},
                new Reservation {ReservationDate = DateTime.Now.AddDays(8),},
                new Reservation {ReservationDate = DateTime.Now.AddDays(9)},

            };

            int i = 0;
            foreach (Reservation r in reservations)
            {
                Trip trip = context.Trip.Skip(i).FirstOrDefault();
                Client client = context.Client.Skip(i).FirstOrDefault();

                r.Trip = trip;
                r.Client = client;

                context.Reservation.Add(r);

                i++;
            }

            context.SaveChanges();
        }
    }
}