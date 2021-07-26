using AutoMapper;
using PostOffice.API.Model.Infrastructure;
using PostOffice.API.Model.Shipment;
using PostOffice.DAL.DataModels.Entity;
using PostOffice.DAL.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOffice.API.Logic.ShipmentLogic
{
	public class ShipmentLogic : IShipmentLogic
	{
		public readonly IShipmentRepository _shipmentRepository;
		public readonly IBagRepository _bagRepository;
		private readonly IMapper _mapper;
		public ShipmentLogic(IShipmentRepository shipmentRepository, IMapper mapper, IBagRepository bagRepository)
		{
			_shipmentRepository = shipmentRepository;
			_mapper = mapper;
			_bagRepository = bagRepository;
		}
		public async Task<APIResponse<IEnumerable<ShipmentAPIModel>>> GetShipmentsAsync(int? lastId)
		{
			try
			{
				IEnumerable<Shipment> result = await _shipmentRepository.GetShipmentsAsync(lastId);
				return new APIResponse<IEnumerable<ShipmentAPIModel>>
				{
					IsSuccess = true,
					Data = _mapper.Map<IEnumerable<ShipmentAPIModel>>(result)
				};
			}
			catch (Exception e)
			{
				return new APIResponse<IEnumerable<ShipmentAPIModel>>
				{
					IsSuccess = false,
					Error = e.Message
				};
			}

		}

		public async Task<APIResponse<ShipmentAPIModel>> CreateOrUpdateShipmentAsync(ShipmentAPIModel model)
		{
			try
			{
				Shipment mappedModel;
				if (model.Id == null)
                {
					mappedModel = _mapper.Map<Shipment>(model);
				} else
                {
					mappedModel = await _shipmentRepository.GetInProgressShipment(model.Id.Value);
					if (mappedModel == null)
					{
						throw new Exception("Shipment is already finished or doesn't exist.");
					}
					mappedModel = _mapper.Map(model, mappedModel);
				}
				if (await _shipmentRepository.IsInUseAsync(model.ShipmentNumber, model.Id))
				{
					throw new Exception("Shipment number is not unique");
				}
				Shipment result = model.Id == null ? await _shipmentRepository.AddAsync(mappedModel)
					: await _shipmentRepository.UpdateAsync(mappedModel);
				return new APIResponse<ShipmentAPIModel>
				{
					IsSuccess = true,
					Data = _mapper.Map<ShipmentAPIModel>(result)
				};
			}
			catch (Exception e)
			{
				return new APIResponse<ShipmentAPIModel>
				{
					IsSuccess = false,
					Error = e.Message
				};
			}

		}

		public async Task<APIResponse> FinishShipmentAsync(int id)
        {
			try
			{
				Shipment existing = await _shipmentRepository.GetInProgressShipment(id);
				if (existing == null)
				{
					throw new Exception("Shipment is already finished or doesn't exist.");
				}
				if (existing.FlightDate < DateTime.Now)
				{
					throw new Exception("Shipment date has to be in the future.");
				}
				if (await _bagRepository.HasAnyEmptyParcelBags(id) || !await _bagRepository.HasAnyBags(id))
                {
					throw new Exception("Shipment has empty parcel bags or no bags at all.");
				}

				existing.Status = DAL.DataModels.Enums.ShipmentStatus.Finished;
				await _shipmentRepository.UpdateAsync(existing);

				return new APIResponse
				{
					IsSuccess = true
				};
			}
			catch (Exception e)
			{
				return new APIResponse
				{
					IsSuccess = false,
					Error = e.Message
				};
			}
		}
	}
}
