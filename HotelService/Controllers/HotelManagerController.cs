using HotelService.Application.Features.HotelManagers.Commands;
using HotelService.Application.Features.HotelManagers.DTOs;
using HotelService.Application.Features.HotelManagers.Queries;
using HotelService.Application.Features.Hotels.Commands;
using HotelService.Application.Features.Hotels.Queries;
using HotelService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Wrappers;
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
            var manager = await _mediator.Send(new GetHotelManagerByHotelIdQuery { HotelId = hotelId });
            if (manager == null)
            {
                return NotFound(new ApiResponse<string>("No manager found for this hotel."));
            }
            return Ok(new ApiResponse<HotelManagerDto>(manager));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetHotelManagerByHotelId(Guid id)
        {
            var manager = await _mediator.Send(new GetHotelManagerQuery { Id = id });
            if (manager == null)
            {
                return NotFound(new ApiResponse<string>("No manager found for this hotel."));
            }
            return Ok(new ApiResponse<HotelManagerDto>(manager));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHotelManager([FromBody] CreateHotelManagerCommand command)
        {
            if (command == null)
            {
                return BadRequest(new ApiResponse<string>("Invalid data provided."));
            }

            var managerId = await _mediator.Send(command);
            if (managerId == Guid.Empty)
            {
                return BadRequest(new ApiResponse<string>("Failed to create manager."));
            }

            return Ok(new ApiResponse<Guid>(managerId));
        }

        [HttpDelete("delete/{contactId}")]
        public async Task<IActionResult> DeleteHotelManager(Guid managerId)
        {
            await _mediator.Send(new DeleteHotelManagerCommand { Id = managerId });

            return NoContent();
        }
    }
}
