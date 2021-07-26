using PostOffice.API.Model.Bag;
using PostOffice.API.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOffice.API.Logic.BagLogic
{
    public interface IBagLogic
    {
        Task<APIResponse<IEnumerable<BagWithParcelCountAPIModel>>> GetBagsAsync(int shipmentId, int? lastId);
        Task<APIResponse<BagAPIModel>> CreateOrUpdateBagAsync(BagAPIModel model);
    }
}
