using Microsoft.EntityFrameworkCore;
using Trips.Data;
using Trips.Models;

namespace Trips.Repositories.RepositoryTrip
{
    public class TripRepository : ITripRepository
    {
        private readonly TripReservationDbContext _context;
        public TripRepository(TripReservationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Trip> GetAll()
        {
            return _context.Trip.ToList();
        }

        public Trip GetById(int tripId)
        {
            return _context.Trip.Find(tripId);
        }


        public void Insert(Trip trip)
        {
            _context.Trip.Add(trip);
        }




        public void Update(Trip trip)
        {
            _context.Entry(trip).State = EntityState.Modified;
        }


        public void Delete(int tripId)
        {
            Trip trip = _context.Trip.Find(tripId);
             
            if (trip != null)
            {
                _context.Trip.Remove(trip);
            }

        }

        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;



        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
