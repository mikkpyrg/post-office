using PostOffice.DAL.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostOffice.DAL.DataModels.Entity
{
	public class Shipment
	{
		public int Id { get; set; }
		[MaxLength(10)]
		[MinLength(10)]
		[Required]
		public string ShipmentNumber { get; set; }
		[Required]
		public Airport Airport { get; set; }
		public ShipmentStatus Status { get; set; }
		[MaxLength(6)]
		[MinLength(6)]
		[Required]
		public string FlightNumber { get; set; }
		public DateTime FlightDate { get; set; }
		public List<Bag> Bags { get; set; }

	}
}
