using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Trips.Data;
using Trips.Models;
using Trips.Repositories.RepositoryTrip;
using Trips.Services.TripService;
using Trips.ViewModel;

namespace Trips.Controllers
{
	public class HomeController : Controller
	{
       

        private ITripService _tripService;

        public HomeController(ITripService tripService)
        {
            _tripService = tripService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            var trips = _tripService.GetAllTrips(); 
            return View(trips); 
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}