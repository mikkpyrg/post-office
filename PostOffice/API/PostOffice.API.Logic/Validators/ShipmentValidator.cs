using FluentValidation;
using PostOffice.API.Model.Shipment;
using System;

namespace PostOffice.API.Logic.Validators
{
	public class ShipmentValidator : AbstractValidator<ShipmentAPIModel>
	{
		public ShipmentValidator()
		{
			RuleFor(x => x.ShipmentNumber)
				.NotEmpty()
				.Matches("^[0-9]{3}-[0-9]{6}$")
				.WithMessage("Shipment number must match format “XXX-XXXXXX”, where X - digit.");

			RuleFor(x => x.Airport)
				.IsInEnum()
				.NotEmpty()
				.WithMessage("Airport must be selected.");

			RuleFor(x => x.FlightNumber)
				.NotEmpty()
				.Matches("^[a-zA-Z]{2}[0-9]{4}$")
				.WithMessage("Flight number must be in Format “LLNNNN”, where L – letter, N – digit");
		}
	}
}
