using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReportService.Application.Features.Commands;
using ReportService.Application.Features.DTOs;
using ReportService.Application.Features.Queries;
using ReportService.Application.Interfaces;
using Shared.Messaging;
using Shared.Wrappers;

namespace ReportService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IReportProducer _reportProducer;

        public ReportsController(IMediator mediator, IReportProducer reportProducer)
        {
            _mediator = mediator;
            _reportProducer = reportProducer;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportCommand command)
        {
            if (command == null)
                return BadRequest(new ApiResponse<string>(null, "report data is null", false, 400));

            var result = await _mediator.Send(command);

            var createReportMessage = new CreateReportMessage
            {
                ReportId = result,
                Location = command.Location
            };
            await _reportProducer.PublishReportRequestAsync(createReportMessage);

            return Ok(new ApiResponse<Guid>(result, "successfully"));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetReport(Guid id)
        {
            var query = new GetReportByIdQuery();
            var report = await _mediator.Send(query);

            if (report == null)
                return NotFound(new ApiResponse<object>(null, "report not found", false, 404));

            return Ok(new ApiResponse<ReportDto>(report, "report fetched successfully"));
        }   
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllReport()
        {
            var query = new GetAllReportsQuery();
            var report = await _mediator.Send(query);

            if (report == null)
                return NotFound(new ApiResponse<object>(null, "report not found", false, 404));

            return Ok(new ApiResponse<List<ReportDto>>(report, "report fetched successfully"));
        }






    }
}
