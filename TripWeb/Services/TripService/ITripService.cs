using Trips.Models;

namespace Trips.Services.TripService
{
    public interface ITripService
    {
        IEnumerable<Trip> GetAllTrips();
        Trip GetByIdTrips(int tripId);
        void UpdateTrips(Trip trip);
        void DeleteTrips(int tripId);
        void SaveTrips();
        void CreateTrips(Trip trip);
    }
}
