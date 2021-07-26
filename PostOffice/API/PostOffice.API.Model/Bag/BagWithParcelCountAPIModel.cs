using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOffice.API.Model.Bag
{
    public class BagWithParcelCountAPIModel : BagAPIModel
	{
        public int ParcelCount { get; set; }
    }
}
