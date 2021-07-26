using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOffice.API.Model.Bag
{
    public class BagAPIModel
    {
		public int? Id { get; set; }
		public string BagNumber { get; set; }
		public int? CountOfLetters { get; set; }
		public decimal? Weight { get; set; }
		public decimal? Price { get; set; }
		public int ShipmentId { get; set; }
		public BagTypeAPIModel? BagType { get; set; }
    }
}
