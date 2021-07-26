using PostOffice.API.Model.Infrastructure;
using PostOffice.API.Model.Parcel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOffice.API.Logic.BagLogic
{
    public interface IParcelLogic
    {
        Task<APIResponse<IEnumerable<ParcelAPIModel>>> GetParcelsAsync(int bagId, int? lastId);
        Task<APIResponse<ParcelAPIModel>> CreateOrUpdateParcelAsync(ParcelAPIModel model);
    }
}
