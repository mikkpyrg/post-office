using PostOffice.DAL.DataModels.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOffice.DAL.Repositories.Repositories
{
    public interface IBagRepository
    {
        Task<IEnumerable<Tuple<Bag, int>>> GetBagsWithParcelCountAsync(int shipmentId, int? lastId);
        Task<Bag> GetBagAsync(int id);
        Task<Bag> AddAsync(Bag model);
        Task<Bag> UpdateAsync(Bag model);
        Task<bool> IsInUseAsync(string bagNumber, int? id);
        Task<bool> CanAcceptParcels(int id);
        Task<bool> HasAnyEmptyParcelBags(int shipmentId);
        Task<bool> HasAnyBags(int shipmentId);
    }
}
