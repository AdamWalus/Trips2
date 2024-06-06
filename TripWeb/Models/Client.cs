using System.ComponentModel.DataAnnotations;

namespace Trips.Models
{
    public class Client
    {
        [Key]
        public int clientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public ICollection<Reservation> Reservation { get; set; }
    }
}
