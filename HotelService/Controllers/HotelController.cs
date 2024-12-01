using HotelService.Application.Features.Hotels.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HotelService.Application.Features.Hotels.DTOs;
using HotelService.Application.Features.Hotels.Queries.GetHotelById;
using Shared.Wrappers;
using HotelService.Application.Features.Hotels.Queries;

namespace HotelService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var query = new GetAllHotelsQuery();
            var hotels = await _mediator.Send(query);
            return Ok(new ApiResponse<List<HotelDto>>(hotels, "Hotels fetched successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel(Guid id)
        {
            var query = new GetHotelByIdQuery(id);
            var hotel = await _mediator.Send(query);

            if (hotel == null)
                return NotFound(new ApiResponse<object>(null, "Hotel not found", false, 404));

            return Ok(new ApiResponse<HotelDto>(hotel, "Hotel fetched successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
        {
            if (command == null)
                return BadRequest(new ApiResponse<string>(null, "Hotel data is null", false, 400));

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetHotel), new { id = result }, new ApiResponse<Guid>(result, "Hotel created successfully"));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] UpdateHotelCommand command)
        {
            if (id != command.Id)
                return BadRequest(new ApiResponse<object>(null, "Hotel ID mismatch", false, 400));

            var result = await _mediator.Send(command);
            return Ok(new ApiResponse<HotelDto>(result, "Hotel updated successfully"));
        }

        // DELETE: api/hotel/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            var command = new DeleteHotelCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }

    }
}
