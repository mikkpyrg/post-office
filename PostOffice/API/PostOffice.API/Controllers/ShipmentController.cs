using Microsoft.AspNetCore.Mvc;
using PostOffice.API.Logic.ShipmentLogic;
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
	public class ShipmentController : BaseController
	{
		private readonly IShipmentLogic _logic;
		public ShipmentController(IShipmentLogic logic)
		{
			_logic = logic;

		}
		[HttpGet("shipments")]
		public async Task<ActionResult<APIResponse<IEnumerable<ShipmentAPIModel>>>> GetShipmentsAsync(int? lastId = null)
		{
			APIResponse<IEnumerable<ShipmentAPIModel>> response = await _logic.GetShipmentsAsync(lastId);

			return OkIfSuccess(response);
		}

		[HttpPost("shipment")]
		public async Task<ActionResult<APIResponse<ShipmentAPIModel>>> CreateOrUpdateShipmentAsync(ShipmentAPIModel model)
		{
			APIResponse<ShipmentAPIModel> response = await _logic.CreateOrUpdateShipmentAsync(model);

			return OkIfSuccess(response);
		}

		[HttpPost("shipment/{id}/finish")]
		public async Task<ActionResult<APIResponse>> FinishShipmentAsync(int id)
		{
			APIResponse response = await _logic.FinishShipmentAsync(id);

			return OkIfSuccess(response);
		}
	}
}
