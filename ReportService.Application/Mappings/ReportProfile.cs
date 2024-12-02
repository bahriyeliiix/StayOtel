using AutoMapper;
using ReportService.Application.Features.Commands;
using ReportService.Application.Features.DTOs;

namespace ReportService.Application.Mappings
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportData, ReportDto>().ReverseMap();
            CreateMap<ReportDto, ReportData>().ReverseMap();

            CreateMap<ReportData, CreateReportCommand>().ReverseMap();
            CreateMap<CreateReportCommand, ReportData>().ReverseMap();


            CreateMap<ReportData, CreateReportCommand>().ReverseMap();
            CreateMap<CreateReportCommand, ReportData>().ReverseMap();

        }
    }
}
