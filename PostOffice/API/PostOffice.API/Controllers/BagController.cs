using Microsoft.AspNetCore.Mvc;
using PostOffice.API.Logic.BagLogic;
using PostOffice.API.Model.Bag;
using PostOffice.API.Model.Infrastructure;
using PostOffice.API.Model.Shipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class BagController : BaseController
    {
		private readonly IBagLogic _logic;
		public BagController(IBagLogic logic)
		{
			_logic = logic;

		}
		[HttpGet("shipment/{shipmentId}/bags")]
		public async Task<ActionResult<APIResponse<IEnumerable<BagWithParcelCountAPIModel>>>> GetBagsAsync(int shipmentId, int? lastId = null)
		{
            APIResponse<IEnumerable<BagWithParcelCountAPIModel>> response = await _logic.GetBagsAsync(shipmentId, lastId);

			return OkIfSuccess(response);
		}

        [HttpPost("bag")]
        public async Task<ActionResult<APIResponse<BagAPIModel>>> CreateOrUpdateShipmentAsync(BagAPIModel model)
        {
            APIResponse<BagAPIModel> response = await _logic.CreateOrUpdateBagAsync(model);

            return OkIfSuccess(response);
        }
    }
}
