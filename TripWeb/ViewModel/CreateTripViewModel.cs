using FluentValidation;
using Trips.Models;

namespace Trips.ViewModel
{
    public class CreateTripViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
