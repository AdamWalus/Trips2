using AutoMapper;
using Trips.Models;
using Trips.ViewModel;


namespace Trips
{
    public class TripMapping : Profile
    {
        public TripMapping()
        {
            CreateMap<CreateTripViewModel, Trip>()
                .ForMember(d => d.tripId, o => o.MapFrom(s => s.Id))
                .ReverseMap();
        }
    }
}