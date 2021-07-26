using Microsoft.AspNetCore.Mvc;
using PostOffice.API.Logic.BagLogic;
using PostOffice.API.Model.Bag;
using PostOffice.API.Model.Infrastructure;
using PostOffice.API.Model.Parcel;
using PostOffice.API.Model.Shipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ParcelController : BaseController
    {
		private readonly IParcelLogic _logic;
		public ParcelController(IParcelLogic logic)
		{
			_logic = logic;

		}
		[HttpGet("bag/{bagId}/parcels")]
		public async Task<ActionResult<APIResponse<IEnumerable<ParcelAPIModel>>>> GetParcelsAsync(int bagId, int? lastId = null)
		{
            APIResponse<IEnumerable<ParcelAPIModel>> response = await _logic.GetParcelsAsync(bagId, lastId);

			return OkIfSuccess(response);
		}

        [HttpPost("parcel")]
        public async Task<ActionResult<APIResponse<ParcelAPIModel>>> CreateOrUpdateShipmentAsync(ParcelAPIModel model)
        {
            APIResponse<ParcelAPIModel> response = await _logic.CreateOrUpdateParcelAsync(model);

            return OkIfSuccess(response);
        }
    }
}
