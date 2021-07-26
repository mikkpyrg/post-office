using AutoMapper;
using PostOffice.API.Model.Bag;
using PostOffice.API.Model.Infrastructure;
using PostOffice.API.Model.Parcel;
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
    public class ParcelLogic : IParcelLogic
	{
		public readonly IBagRepository _bagRepository;
		public readonly IShipmentRepository _shipmentRepository;
		public readonly IParcelRepository _parcelRepository;
		private readonly IMapper _mapper;
		public ParcelLogic(IBagRepository bagRepository, IMapper mapper, IShipmentRepository shipmentRepository, IParcelRepository parcelRepository)	
		{
			_bagRepository = bagRepository;
			_mapper = mapper;
			_shipmentRepository = shipmentRepository;
			_parcelRepository = parcelRepository;
		}

		public async Task<APIResponse<IEnumerable<ParcelAPIModel>>> GetParcelsAsync(int bagId, int? lastId)
		{
			try
			{
				IEnumerable<Parcel> result = await _parcelRepository.GetParcelsAsync(bagId, lastId);
				return new APIResponse<IEnumerable<ParcelAPIModel>>
				{
					IsSuccess = true,
					Data = _mapper.Map<IEnumerable<ParcelAPIModel>>(result)
				};
			}
			catch (Exception e)
			{
				return new APIResponse<IEnumerable<ParcelAPIModel>>
				{
					IsSuccess = false,
					Error = e.Message
				};
			}

		}

		public async Task<APIResponse<ParcelAPIModel>> CreateOrUpdateParcelAsync(ParcelAPIModel model)
		{
			try
			{
				if (!await _bagRepository.CanAcceptParcels(model.BagId))
				{
					throw new Exception("Bag can not accept any parcels. Meaning shipment is not in progress, bag's type is not parcel or bag was not found.");
				}
				if (await _parcelRepository.IsInUseAsync(model.ParcelNumber, model.Id))
				{
					throw new Exception("Parcel number is not unique.");
				}
				Parcel mappedModel;
				if (model.Id == null)
                {
					mappedModel = _mapper.Map<Parcel>(model);
				} else
                {
					mappedModel = await _parcelRepository.GetParcelAsync(model.Id.Value);
					if (mappedModel == null)
					{
						throw new Exception("Parcel with this ID doesn't exist.");
					}
					mappedModel = _mapper.Map(model, mappedModel);
				}

				Parcel result = model.Id == null ? await _parcelRepository.AddAsync(mappedModel)
					: await _parcelRepository.UpdateAsync(mappedModel);
				return new APIResponse<ParcelAPIModel>
				{
					IsSuccess = true,
					Data = _mapper.Map<ParcelAPIModel>(result)
				};
			}
			catch (Exception e)
			{
				return new APIResponse<ParcelAPIModel>
				{
					IsSuccess = false,
					Error = e.Message
				};
			}
		}
	}
}
