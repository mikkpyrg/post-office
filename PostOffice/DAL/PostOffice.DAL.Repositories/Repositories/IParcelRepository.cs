using PostOffice.DAL.DataModels.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOffice.DAL.Repositories.Repositories
{
    public interface IParcelRepository
    {
        Task<IEnumerable<Parcel>> GetParcelsAsync(int bagId, int? lastId);
        Task<Parcel> GetParcelAsync(int id);
        Task<Parcel> AddAsync(Parcel model);
        Task<Parcel> UpdateAsync(Parcel model);
        Task<bool> IsInUseAsync(string parcelNumber, int? id);
    }
}
