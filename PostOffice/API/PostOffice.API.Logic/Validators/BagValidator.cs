using FluentValidation;
using PostOffice.API.Model.Bag;
using PostOffice.API.Model.Shipment;
using System;

namespace PostOffice.API.Logic.Validators
{
	public class BagValidator : AbstractValidator<BagAPIModel>
	{
		public BagValidator()
		{
			RuleFor(x => x.BagNumber)
				.NotEmpty()
				.Matches("^[a-zA-Z0-9]{1,15}$")
				.WithMessage("Bag number must be max 15 characters, consist only of letters or numbers.");

			When(x => x.Id == null, () =>
			{
				RuleFor(x => x.BagType)
					.IsInEnum()
					.NotEmpty()
					.WithMessage("Bag type must be selected when creating a new bag.");
			});

			When(x => x.Id != null, () =>
			{
				RuleFor(x => x.BagType)
					.Empty()
					.WithMessage("Can't change Bag type of existing bag.");
			});

			RuleFor(x => x.ShipmentId)
				.NotEmpty()
				.WithMessage("Sir, sir, sir! I need to know what shipment you are putting the bag into.");

			RuleFor(x => x.Price)
				.ScalePrecision(2,18)
				.WithMessage("Price should have at most 2 decimals after comma.");

			RuleFor(x => x.Weight)
				.ScalePrecision(3, 18)
				.WithMessage("Weight should have at most 3 decimals after comma.");

			RuleFor(x => x.CountOfLetters)
				.GreaterThan(0)
				.WithMessage("Letter count has to be bigger than 0.");
		}
	}
}
