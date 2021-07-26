using Microsoft.EntityFrameworkCore;
using PostOffice.DAL.DataModels.Entity;
using PostOffice.DAL.DataModels.Enums;
using PostOffice.DAL.Repositories.EntityFrameworkDataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice.DAL.Repositories.Repositories
{
	public class ShipmentRepository : IShipmentRepository
	{
		private readonly ApplicationDBContext _context;
		public ShipmentRepository(ApplicationDBContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Shipment>> GetShipmentsAsync(int? lastId)
		{
			return await _context.Shipment
				.Where(x => lastId == null || x.Id < lastId.Value)
				.OrderByDescending(x => x.Id)
				.Take(10)
				.ToListAsync();
		}

		public async Task<Shipment> AddAsync(Shipment model)
		{
			model.Status = ShipmentStatus.InProgress;
			await _context.Shipment.AddAsync(model);
			await _context.SaveChangesAsync();
			return model;
		}

		public async Task<bool> IsInUseAsync(string shipmentNumber, int? id)
		{
			return await _context.Shipment.AnyAsync(x => x.ShipmentNumber == shipmentNumber && (id == null || x.Id != id.Value));
		}

		public async Task<Shipment> GetInProgressShipment(int id)
		{
			return await _context.Shipment.FirstOrDefaultAsync(x => x.Id == id && x.Status == ShipmentStatus.InProgress);
		}

		public async Task<Shipment> UpdateAsync(Shipment model)
		{
			_context.Shipment.Update(model);
			await _context.SaveChangesAsync();
			return model;
		}
	}
}
