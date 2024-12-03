using HotelService.Application.Features.Hotels.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HotelService.Application.Features.Hotels.DTOs;
using HotelService.Application.Features.Hotels.Queries.GetHotelById;
using Shared.Wrappers;
using HotelService.Application.Features.Hotels.Queries;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet("get-all")]
        [SwaggerOperation(Summary = "Get a list of all hotels", OperationId = "GetAll")]
        public async Task<IActionResult> GetAllHotels()
        {
            Log.Information("Fetching all hotels...");
            try
            {
                var query = new GetAllHotelsQuery();
                var hotels = await _mediator.Send(query);
                Log.Information("{Count} hotels fetched successfully", hotels.Count);
                return Ok(new ApiResponse<List<HotelDto>>(hotels, "Hotels fetched successfully"));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching all hotels");
                return StatusCode(500, new ApiResponse<string>(null, "An error occurred while fetching hotels", false, 500));
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetHotel(Guid id)
        {
            Log.Information("Fetching hotel with ID: {Id}", id);
            try
            {
                var query = new GetHotelByIdQuery(id);
                var hotel = await _mediator.Send(query);

                if (hotel == null)
                {
                    Log.Warning("Hotel with ID: {Id} not found", id);
                    return NotFound(new ApiResponse<object>(null, "Hotel not found", false, 404));
                }

                Log.Information("Hotel with ID: {Id} fetched successfully", id);
                return Ok(new ApiResponse<HotelDetailDto>(hotel, "Hotel fetched successfully"));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching hotel with ID: {Id}", id);
                return StatusCode(500, new ApiResponse<string>(null, "An error occurred while fetching the hotel", false, 500));
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
        {
            if (command == null)
            {
                Log.Warning("Invalid hotel data provided for creation");
                return BadRequest(new ApiResponse<string>(null, "Hotel data is null", false, 400));
            }

            Log.Information("Attempting to create a new hotel...");
            try
            {
                var result = await _mediator.Send(command);
                Log.Information("Hotel created successfully with ID: {Id}", result);
                return CreatedAtAction(nameof(GetHotel), new { id = result }, new ApiResponse<Guid>(result, "Hotel created successfully"));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while creating a hotel");
                return StatusCode(500, new ApiResponse<string>(null, "An error occurred while creating the hotel", false, 500));
            }
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] UpdateHotelCommand command)
        {
            if (id != command.Id)
            {
                Log.Warning("Hotel ID mismatch for update. Route ID: {RouteId}, Command ID: {CommandId}", id, command.Id);
                return BadRequest(new ApiResponse<object>(null, "Hotel ID mismatch", false, 400));
            }

            Log.Information("Attempting to update hotel with ID: {Id}", id);
            try
            {
                var result = await _mediator.Send(command);
                Log.Information("Hotel with ID: {Id} updated successfully", id);
                return Ok(new ApiResponse<HotelDto>(result, "Hotel updated successfully"));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while updating hotel with ID: {Id}", id);
                return StatusCode(500, new ApiResponse<string>(null, "An error occurred while updating the hotel", false, 500));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            Log.Information("Attempting to delete hotel with ID: {Id}", id);
            try
            {
                var command = new DeleteHotelCommand { Id = id };
                await _mediator.Send(command);
                Log.Information("Hotel with ID: {Id} deleted successfully", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while deleting hotel with ID: {Id}", id);
                return StatusCode(500, new ApiResponse<string>(null, "An error occurred while deleting the hotel", false, 500));
            }
        }
    }
}
