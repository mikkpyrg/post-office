using Microsoft.EntityFrameworkCore;
using PostOffice.DAL.DataModels.Entity;
using PostOffice.DAL.Repositories.EntityFrameworkDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice.DAL.Repositories.Repositories
{
    public class BagRepository : IBagRepository
    {
        private readonly ApplicationDBContext _context;
        public BagRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tuple<Bag, int>>> GetBagsWithParcelCountAsync(int shipmentId, int? lastId)
        {
            return await _context.Bag
                .Include(x => x.Parcels)
                .Where(x => x.ShipmentId == shipmentId && (lastId == null || x.Id < lastId.Value))
                .OrderByDescending(x => x.Id)
                .Take(10)
                .Select(x => Tuple.Create(x, x.Parcels.Count()))
                .ToListAsync();
        }

        public async Task<Bag> GetBagAsync(int id)
        {
            return await _context.Bag.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Bag> AddAsync(Bag model)
        {
            await _context.Bag.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Bag> UpdateAsync(Bag model)
        {
            _context.Bag.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> IsInUseAsync(string bagNumber, int? id)
        {
            return await _context.Bag.AnyAsync(x => x.BagNumber == bagNumber && (id == null || x.Id != id.Value));
        }

        public async Task<bool> CanAcceptParcels(int id)
        {
            return await _context.Bag.AnyAsync(x => x.Id == id && x.BagType == DataModels.Enums.BagType.Parcel && x.Shipment.Status == DataModels.Enums.ShipmentStatus.InProgress);
        }

        public async Task<bool> HasAnyBags(int shipmentId)
        {
            return await _context.Bag.AnyAsync(x => x.ShipmentId == shipmentId);
        }

        public async Task<bool> HasAnyEmptyParcelBags(int shipmentId)
        {
            return await _context.Bag.AnyAsync(x => x.ShipmentId == shipmentId
                && x.BagType == DataModels.Enums.BagType.Parcel
                && !x.Parcels.Any());
        }
    }
}
