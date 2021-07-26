using PostOffice.API.Model.Infrastructure;
using PostOffice.API.Model.Shipment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOffice.API.Logic.ShipmentLogic
{
	public interface IShipmentLogic
	{
		Task<APIResponse<IEnumerable<ShipmentAPIModel>>> GetShipmentsAsync(int? lastId);
		Task<APIResponse<ShipmentAPIModel>> CreateOrUpdateShipmentAsync(ShipmentAPIModel model);
		Task<APIResponse> FinishShipmentAsync(int Id);
	}
}
