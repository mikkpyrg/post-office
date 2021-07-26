using AutoMapper;
using PostOffice.API.Model.Bag;
using PostOffice.API.Model.Infrastructure;
using PostOffice.DAL.DataModels.Entity;
using PostOffice.DAL.DataModels.Enums;
using PostOffice.DAL.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOffice.API.Logic.BagLogic
{
    public class BagLogic : IBagLogic
    {
		public readonly IBagRepository _bagRepository;
		public readonly IShipmentRepository _shipmentRepository;
		private readonly IMapper _mapper;
		public BagLogic(IBagRepository bagRepository, IMapper mapper, IShipmentRepository shipmentRepository)
		{
			_bagRepository = bagRepository;
			_mapper = mapper;
			_shipmentRepository = shipmentRepository;
		}

		public async Task<APIResponse<IEnumerable<BagWithParcelCountAPIModel>>> GetBagsAsync(int shipmentId, int? lastId)
		{
			try
			{
				IEnumerable<Tuple<Bag, int>> result = await _bagRepository.GetBagsWithParcelCountAsync(shipmentId, lastId);
				return new APIResponse<IEnumerable<BagWithParcelCountAPIModel>>
				{
					IsSuccess = true,
					Data = _mapper.Map<IEnumerable<BagWithParcelCountAPIModel>>(result)
				};
			}
			catch (Exception e)
			{
				return new APIResponse<IEnumerable<BagWithParcelCountAPIModel>>
				{
					IsSuccess = false,
					Error = e.Message
				};
			}

		}

		public async Task<APIResponse<BagAPIModel>> CreateOrUpdateBagAsync(BagAPIModel model)
		{
			try
			{
				if (await _shipmentRepository.GetInProgressShipment(model.ShipmentId) == null)
				{
					throw new Exception("Shipment is already finished or doesn't exist.");
				}

				if (await _bagRepository.IsInUseAsync(model.BagNumber, model.Id))
				{
					throw new Exception("Bag number is not unique.");
				}

				Bag mappedModel;
				if (model.Id == null)
                {
					mappedModel = _mapper.Map<Bag>(model);
					// Separately add, because we don't want to overwrite existing bag's type
					mappedModel.BagType = (BagType)model.BagType;
				} else
                {
					mappedModel = await _bagRepository.GetBagAsync(model.Id.Value);
					if (mappedModel == null)
					{
						throw new Exception("Bag with this ID doesn't exist.");
					}
					mappedModel = _mapper.Map(model, mappedModel);
				}
				if (mappedModel.BagType == BagType.Letter && (mappedModel.Price == null || mappedModel.Weight == null || mappedModel.CountOfLetters == null))
				{
					throw new Exception("Letter type bag shouldn't have empty price or weight or letter count.");
				} else if (mappedModel.BagType == BagType.Parcel && (mappedModel.Price != null || mappedModel.Weight != null || mappedModel.CountOfLetters != null)) 
				{
					throw new Exception("Parcel type bag shouldn't have filled price or weight or letter count.");
				}
				Bag result = model.Id == null ? await _bagRepository.AddAsync(mappedModel)
					: await _bagRepository.UpdateAsync(mappedModel);
				return new APIResponse<BagAPIModel>
				{
					IsSuccess = true,
					Data = _mapper.Map<BagAPIModel>(result)
				};
			}
			catch (Exception e)
			{
				return new APIResponse<BagAPIModel>
				{
					IsSuccess = false,
					Error = e.Message
				};
			}
		}
	}
}
