using PostOffice.DAL.DataModels.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.DAL.DataModels.Entity
{
	public class Bag
	{
		public int Id { get; set; }
		[MaxLength(15)]
		[Required]
		public string BagNumber { get; set; }
		public int? CountOfLetters { get; set; }
		[Column(TypeName = "decimal(18, 3)")]
		public decimal? Weight { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal? Price { get; set; }
		public int ShipmentId { get; set; }
		public Shipment Shipment { get; set; }
		public List<Parcel> Parcels { get; set; }
        public BagType BagType { get; set; }
    }
}
