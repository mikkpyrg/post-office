using System;

namespace PostOffice.API.Model.Shipment
{
	public class ShipmentAPIModel
	{
		public int? Id { get; set; }
		public string ShipmentNumber { get; set; }
		public AirportAPIModel Airport { get; set; }
		public ShipmentStatusAPIModel Status { get; set; }
		public string FlightNumber { get; set; }
		public DateTime FlightDate { get; set; }
	}
}
