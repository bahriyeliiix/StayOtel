using AutoMapper;
using ReportService.Application.Features.DTOs;

namespace ReportService.Application.Mappings
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportData, ReportDto>().ReverseMap();
            CreateMap<ReportDto, ReportData>().ReverseMap();
        }
    }
}
