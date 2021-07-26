using PostOffice.DAL.DataModels.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOffice.DAL.Repositories.Repositories
{
	public interface IShipmentRepository
	{
		Task<IEnumerable<Shipment>> GetShipmentsAsync(int? lastId);
		Task<Shipment> AddAsync(Shipment model);
		Task<bool> IsInUseAsync(string shipmentNumber, int? id);
		Task<Shipment> GetInProgressShipment(int id);
		Task<Shipment> UpdateAsync(Shipment model);
	}
}
