using Microsoft.EntityFrameworkCore;
using Trips.Data;
using Trips.Models;
using Trips.Repositories.RepositoryTrip;

namespace Trips.Services.TripService
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _context;
        public TripService(ITripRepository context)
        {
            _context = context  ?? throw new ArgumentException(nameof(context)) ;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return _context.GetAll().ToList();
        }

        public Trip GetByIdTrips(int tripId)
        {
            return _context.GetById(tripId);
        }

       /* public void InsertTrips(Trip trip)
        {
            _context.Insert(trip);
            _context.Save();
        }*/

        public void UpdateTrips(Trip trip)
        {
            _context.Update(trip);
            _context.Save();
        }


        public void DeleteTrips(int tripId)
        {
            Trip trip = _context.GetById(tripId);
            _context.Delete(tripId);
            _context.Save();

        }

        public void CreateTrips(Trip trip)
        {
            _context.Insert(trip);
            _context.Save();
        }

        public void SaveTrips()
        {
            
        }






    
}
}
