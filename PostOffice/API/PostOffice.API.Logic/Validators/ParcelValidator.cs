using FluentValidation;
using PostOffice.API.Model.Parcel;

namespace PostOffice.API.Logic.Validators
{
    public class ParcelValidator : AbstractValidator<ParcelAPIModel>
	{
		public ParcelValidator()
		{
			RuleFor(x => x.ParcelNumber)
				.NotEmpty()
				.Matches("^[a-zA-Z]{2}[0-9]{6}[a-zA-Z]{2}$")
				.WithMessage("Parcel number must match format “LLNNNNNNLL”, where L – letter, N – digit.");

			RuleFor(x => x.BagId)
				.NotEmpty()
				.WithMessage("Which bag should I lob this parcel into sir?");

			RuleFor(x => x.Price)
				.NotNull()
				.ScalePrecision(2,18)
				.WithMessage("Price should have at most 2 decimals after comma.");

			RuleFor(x => x.Weight)
				.NotNull()
				.ScalePrecision(3, 18)
				.WithMessage("Weight should have at most 3 decimals after comma.");

			RuleFor(x => x.RecipientName)
				.NotEmpty()
				.MaximumLength(100)
				.WithMessage("Recipient's name must be filled.");

			RuleFor(x => x.DestinationCountry)
				.NotEmpty()
				.Length(2)
				.WithMessage("Destination country must match 2 letter format e.g. “EE”, “LV”, “FI”.");
		}
	}
}
