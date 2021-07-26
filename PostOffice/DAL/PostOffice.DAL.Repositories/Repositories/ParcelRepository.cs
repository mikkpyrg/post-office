using Microsoft.EntityFrameworkCore;
using PostOffice.DAL.DataModels.Entity;
using PostOffice.DAL.Repositories.EntityFrameworkDataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostOffice.DAL.Repositories.Repositories
{
    public class ParcelRepository : IParcelRepository
    {
        private readonly ApplicationDBContext _context;
        public ParcelRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Parcel>> GetParcelsAsync(int bagId, int? lastId)
        {
            return await _context.Parcel
                .Where(x => x.BagId == bagId && (lastId == null || x.Id < lastId.Value))
                .OrderByDescending(x => x.Id)
                .Take(10)
                .ToListAsync();
        }

        public async Task<Parcel> GetParcelAsync(int id)
        {
            return await _context.Parcel.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Parcel> AddAsync(Parcel model)
        {
            await _context.Parcel.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Parcel> UpdateAsync(Parcel model)
        {
            _context.Parcel.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> IsInUseAsync(string parcelNumber, int? id)
        {
            return await _context.Parcel.AnyAsync(x => x.ParcelNumber == parcelNumber && (id == null || x.Id != id.Value));
        }
    }
}
