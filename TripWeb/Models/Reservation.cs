using System.ComponentModel.DataAnnotations;

namespace Trips.Models
{
    public class Reservation
    {
        [Key]
        public int reservationId {  get; set; }
        public DateTime ReservationDate {  get; set; }

        public Client Client {  get; set; }

        public Trip Trip { get; set; }
        

    }
}
