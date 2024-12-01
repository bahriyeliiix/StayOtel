using AutoMapper;
using HotelService.Application.Features.Hotels.DTOs;
using HotelService.Domain.Entities;
using HotelService.Application.Features.Hotels.Commands;

namespace HotelService.Application.Mapping
{
    public class HotelServiceMappingProfile : Profile
    {
        public HotelServiceMappingProfile()
        {
            // Hotel Mappings
            CreateMap<Hotel, HotelDto>();
            CreateMap<HotelDto, Hotel>();

            CreateMap<Hotel, CreateHotelCommand>();
            CreateMap<CreateHotelCommand, Hotel>();

            //// HotelManager Mappings
            //CreateMap<HotelManager, HotelManagerDto>();
            //CreateMap<HotelManagerDto, HotelManager>();
            //CreateMap<HotelManager, CreateHotelManagerCommand>();

            //// HotelContact Mappings
            //CreateMap<HotelContact, HotelContactDto>();
            //CreateMap<HotelContactDto, HotelContact>();
            //CreateMap<HotelContact, CreateHotelContactCommand>();
        }
    }
}
