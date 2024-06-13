using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Trips.Data;
using Trips.Models;
using Trips.Repositories.RepositoryTrip;
using Trips.Services.TripService;
using Trips.ViewModel;


namespace Trips.Controllers
{
    public class TripsController : Controller
    {
        private  readonly ITripService _tripService;
        private readonly ITripRepository _tripRepository;
        private readonly IValidator<CreateTripViewModel> _validator;
        private readonly IMapper _mapper;

        public TripsController(ITripService tripService, IValidator<CreateTripViewModel> validator, IMapper mapper)
        {
            _tripService = tripService;
            _validator = validator;
            _mapper = mapper;

        }
        [Authorize (Roles="admin")]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Create(CreateTripViewModel tripViewModel)
        {
            var validationResult = _validator.Validate(tripViewModel);

            if (!validationResult.IsValid)
            {
                //Walidacja nie powiodła sie
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(tripViewModel);
            }

            var trip = _mapper.Map<Trip>(tripViewModel);
            //Walidacja powiodla sie, zapisz
            _tripService.CreateTrips(trip);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Details(int tripId)
        {
            /*var trip = _tripService.GetByIdTrips(tripId);
            if (trip == null){return NotFound();}

            return View("Details", trip);
*/
            var trips = _tripService.GetAllTrips();



            var trip = trips.FirstOrDefault(trip => trip.tripId == tripId);
            if (trip != null)
            {
                var viewModel = new TripDetailsViewModel
                {
                    Id = trip.tripId,
                    Title = trip.Title,
                    StartDate = trip.StartDate,
                    EndDate = trip.EndDate,
                    Price = 999,
                    Destination = trip.Destination,
                    Description = trip.Description
                };


                return View(viewModel);
            }





           return NotFound();

        }

        [HttpGet]
        public ActionResult Update(int tripId)
        {
            var trip = _tripService.GetByIdTrips(tripId);
            if (trip == null){return NotFound();}

            return View("Update", trip);
        }



        [HttpPost]
        public ActionResult UpdateTrip(Trip trip)
        {
            if (ModelState.IsValid)
            {
                _tripService.UpdateTrips(trip);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Details", trip); 
            }
        }


        [HttpGet]
        public ActionResult Delete(int tripId)
        {
            Trip trip = _tripService.GetByIdTrips(tripId);
            if (trip == null){return NotFound();}

            _tripService.DeleteTrips(tripId);

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Index()
        {
            var trips = _tripRepository.GetAll();



            List<TripDetailsViewModel> viewModels = trips.Select(t => new TripDetailsViewModel
            {
                Id = t.tripId,
                Title = t.Title,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                Price = 999,
                Destination = t.Destination,
            }).ToList();




            return View(viewModels);
        }


   



    }
}
