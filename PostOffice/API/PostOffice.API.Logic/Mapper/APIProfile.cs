using AutoMapper;
using PostOffice.API.Model.Bag;
using PostOffice.API.Model.Parcel;
using PostOffice.API.Model.Shipment;
using PostOffice.DAL.DataModels.Entity;
using System;

namespace PostOffice.API.Logic.Mapper
{
	public class APIProfile : Profile
	{
		public APIProfile()
		{
			CreateMap<Shipment, ShipmentAPIModel>();
			CreateMap<ShipmentAPIModel, Shipment>()
				.ForMember(x => x.Id, opt => opt.Ignore())
				.ForMember(x => x.Status, opt => opt.Ignore());

			CreateMap<Bag, BagAPIModel>();
			CreateMap<BagAPIModel, Bag>()
				.ForMember(x => x.Id, opt => opt.Ignore())
				.ForMember(x => x.BagType, opt => opt.Ignore());
			CreateMap<Bag, BagWithParcelCountAPIModel>();
			CreateMap<Tuple<Bag, int>, BagWithParcelCountAPIModel>()
				.ForMember(x => x.ParcelCount, opt => opt.MapFrom(s => s.Item2))
				.AfterMap((src, dst, context) => context.Mapper.Map<Bag, BagWithParcelCountAPIModel>(src.Item1, dst));

			CreateMap<Parcel, ParcelAPIModel>();
			CreateMap<ParcelAPIModel, Parcel>()
				.ForMember(x => x.Id, opt => opt.Ignore());
		}
	}
}
