using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.DAL.DataModels.Entity
{
	public class Parcel
	{
		public int Id { get; set; }
		[MaxLength(10)]
		[MinLength(10)]
		[Required]
		public string ParcelNumber { get; set; }
		[MaxLength(100)]
		[Required]
		public string RecipientName { get; set; }
		[MaxLength(2)]
		[MinLength(2)]
		[Required]
		public string DestinationCountry { get; set; }
		[Required]
		[Column(TypeName = "decimal(18, 3)")]
		public decimal Weight { get; set; }
		[Required]
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Price { get; set; }
		public int BagId { get; set; }
		public Bag Bag { get; set; }

	}
}
