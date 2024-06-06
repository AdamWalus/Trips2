using Trips.Models;

namespace Trips.Repositories.RepositoryTrip
{
    public interface ITripRepository
    {


        IEnumerable<Trip> GetAll();
        Trip GetById(int tripId);
        void Insert(Trip trip);
        void Update(Trip trip);
        void Delete(int tripId);
        void Save();
    }
}
