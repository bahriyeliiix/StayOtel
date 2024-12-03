using HotelService.Application.Features.HotelManagers.Commands;
using HotelService.Application.Features.HotelManagers.DTOs;
using HotelService.Application.Features.HotelManagers.Queries;
using HotelService.Application.Features.Hotels.Commands;
using HotelService.Application.Features.Hotels.Queries;
using HotelService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Wrappers;
using Serilog;
using System;
using System.Threading.Tasks;

namespace HotelService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelManagerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-by-hotelId/{hotelId}")]
        public async Task<IActionResult> GetHotelManager(Guid hotelId)
        {
            Log.Information("Fetching manager for hotel with ID: {HotelId}", hotelId);
            try
            {
                var manager = await _mediator.Send(new GetHotelManagerByHotelIdQuery { HotelId = hotelId });
                if (manager == null)
                {
                    Log.Warning("No manager found for hotel with ID: {HotelId}", hotelId);
                    return NotFound(new ApiResponse<string>("No manager found for this hotel."));
                }

                Log.Information("Manager for hotel with ID: {HotelId} fetched successfully", hotelId);
                return Ok(new ApiResponse<HotelManagerDto>(manager));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching manager for hotel with ID: {HotelId}", hotelId);
                return StatusCode(500, new ApiResponse<string>(null, "An error occurred while fetching the manager.", false, 500));
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetHotelManagerByHotelId(Guid id)
        {
            Log.Information("Fetching manager with ID: {ManagerId}", id);
            try
            {
                var manager = await _mediator.Send(new GetHotelManagerQuery { Id = id });
                if (manager == null)
                {
                    Log.Warning("No manager found with ID: {ManagerId}", id);
                    return NotFound(new ApiResponse<string>("No manager found for this ID."));
                }

                Log.Information("Manager with ID: {ManagerId} fetched successfully", id);
                return Ok(new ApiResponse<HotelManagerDto>(manager));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching manager with ID: {ManagerId}", id);
                return StatusCode(500, new ApiResponse<string>(null, "An error occurred while fetching the manager.", false, 500));
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHotelManager([FromBody] CreateHotelManagerCommand command)
        {
            if (command == null)
            {
                Log.Warning("Invalid data provided for creating manager");
                return BadRequest(new ApiResponse<string>("Invalid data provided."));
            }

            Log.Information("Attempting to create a new hotel manager...");
            try
            {
                var managerId = await _mediator.Send(command);
                if (managerId == Guid.Empty)
                {
                    Log.Warning("Failed to create a new hotel manager");
                    return BadRequest(new ApiResponse<string>("Failed to create manager."));
                }

                Log.Information("Hotel manager created successfully with ID: {ManagerId}", managerId);
                return Ok(new ApiResponse<Guid>(managerId));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while creating a new hotel manager");
                return StatusCode(500, new ApiResponse<string>(null, "An error occurred while creating the manager.", false, 500));
            }
        }

        [HttpDelete("delete/{managerId}")]
        public async Task<IActionResult> DeleteHotelManager(Guid managerId)
        {
            Log.Information("Attempting to delete manager with ID: {ManagerId}", managerId);
            try
            {
                await _mediator.Send(new DeleteHotelManagerCommand { Id = managerId });
                Log.Information("Manager with ID: {ManagerId} deleted successfully", managerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while deleting manager with ID: {ManagerId}", managerId);
                return StatusCode(500, new ApiResponse<string>(null, "An error occurred while deleting the manager.", false, 500));
            }
        }
    }
}
