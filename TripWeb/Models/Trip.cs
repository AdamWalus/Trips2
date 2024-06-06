using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Trips.Models
{
    public class Trip
    {
        [Key]
        public int tripId { get; set; }
        public string Destination { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public ICollection<Reservation> Reservation { get; set; }


    }
    


}
