using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ReportService.Application.Features.DTOs;

namespace ReportService.Application.Features.Queries
{
    public class GetAllReportsQuery : IRequest<List<ReportDto>>
    {
    }
}
