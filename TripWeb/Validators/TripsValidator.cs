using FluentValidation;
using Trips.ViewModel;

namespace Trips.Validators
{
    public class TripsValidator : AbstractValidator<CreateTripViewModel>
    {
        public TripsValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Please specify a title");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Destination)
                .NotEmpty().WithMessage("Please specify a destination");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Please specify a start date")
                .LessThan(x => x.EndDate).WithMessage("Start date must be before end date");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Please specify an end date")
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date");
        }
    }
}