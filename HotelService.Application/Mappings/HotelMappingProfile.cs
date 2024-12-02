using AutoMapper;
using HotelService.Application.Features.Hotels.DTOs;
using HotelService.Domain.Entities;
using HotelService.Application.Features.Hotels.Commands;
using HotelService.Application.Features.HotelManagers.DTOs;
using HotelService.Application.Features.HotelManagers.Commands;
using HotelService.Application.Features.Hotels.Queries;

namespace HotelService.Application.Mapping
{
    public class HotelServiceMappingProfile : Profile
    {
        public HotelServiceMappingProfile()
        {
            // Hotel Mappings
            CreateMap<Hotel, HotelDto>();
            CreateMap<HotelDto, Hotel>();

            CreateMap<Hotel, HotelDetailDto>();
            CreateMap<HotelDetailDto, Hotel>();

            CreateMap<Hotel, CreateHotelCommand>();
            CreateMap<CreateHotelCommand, Hotel>();

            // HotelManager Mappings
            CreateMap<HotelManager, HotelManagerDto>();
            CreateMap<HotelManagerDto, HotelManager>();


            CreateMap<HotelManager, CreateHotelManagerCommand>();
            CreateMap<CreateHotelManagerCommand, HotelManager>();

            // HotelContact Mappings
            CreateMap<HotelContact, HotelContactDto>();
            CreateMap<HotelContactDto, HotelContact>();

            CreateMap<HotelContact, CreateHotelContactCommand>();
            CreateMap<CreateHotelContactCommand, HotelContact>();
        }
    }
}
