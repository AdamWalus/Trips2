namespace Trips.ViewModel
{
	public class TripDetailsViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Location { get; set; }
		public int Price { get; set; }
		public string Description { get; set; }
		public string Destination { get; set; }
	}
}

