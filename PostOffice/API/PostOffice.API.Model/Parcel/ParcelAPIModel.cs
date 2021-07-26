using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOffice.API.Model.Parcel
{
    public class ParcelAPIModel
    {
		public int? Id { get; set; }
		public string ParcelNumber { get; set; }
		public string RecipientName { get; set; }
		public string DestinationCountry { get; set; }
		public decimal Weight { get; set; }
		public decimal Price { get; set; }
		public int BagId { get; set; }
	}
}
