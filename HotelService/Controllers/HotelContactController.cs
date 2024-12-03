using HotelService.Application.Features.Hotels.Commands;
using HotelService.Application.Features.Hotels.Queries;
using HotelService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared.Wrappers;
using System;
using System.Threading.Tasks;

namespace HotelService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HotelContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-by-hotelId/{hotelId}")]
        public async Task<IActionResult> GetHotelContacts(Guid hotelId)
        {
            Log.Information("Fetching contacts for hotelId: {HotelId}", hotelId);
            try
            {
                var contacts = await _mediator.Send(new GetHotelContactsQuery { HotelId = hotelId });
                if (contacts == null || contacts.Count == 0)
                {
                    Log.Warning("No contacts found for hotelId: {HotelId}", hotelId);
                    return NotFound(new ApiResponse<string>("No contacts found for this hotel."));
                }

                Log.Information("Fetched {Count} contacts for hotelId: {HotelId}", contacts.Count, hotelId);
                return Ok(new ApiResponse<List<HotelContactDto>>(contacts));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while fetching contacts for hotelId: {HotelId}", hotelId);
                return StatusCode(500, new ApiResponse<string>("An error occurred while fetching contacts."));
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHotelContact([FromBody] CreateHotelContactCommand command)
        {
            if (command == null)
            {
                Log.Warning("Invalid data provided for creating a hotel contact.");
                return BadRequest(new ApiResponse<string>("Invalid data provided."));
            }

            Log.Information("Attempting to create a hotel contact for hotelId: {HotelId}", command.HotelId);
            try
            {
                var contactId = await _mediator.Send(command);
                if (contactId == Guid.Empty)
                {
                    Log.Warning("Failed to create contact for hotelId: {HotelId}", command.HotelId);
                    return BadRequest(new ApiResponse<string>("Failed to create contact."));
                }

                Log.Information("Successfully created a contact with contactId: {ContactId} for hotelId: {HotelId}", contactId, command.HotelId);
                return CreatedAtAction(nameof(GetHotelContacts), new { hotelId = command.HotelId }, new ApiResponse<Guid>(contactId));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while creating a contact for hotelId: {HotelId}", command.HotelId);
                return StatusCode(500, new ApiResponse<string>("An error occurred while creating the contact."));
            }
        }

        [HttpDelete("delete/{contactId}")]
        public async Task<IActionResult> DeleteHotelContact(Guid contactId)
        {
            Log.Information("Attempting to delete contact with contactId: {ContactId}", contactId);
            try
            {
                await _mediator.Send(new DeleteHotelContactCommand { ContactId = contactId });
                Log.Information("Successfully deleted contact with contactId: {ContactId}", contactId);
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while deleting contact with contactId: {ContactId}", contactId);
                return StatusCode(500, new ApiResponse<string>("An error occurred while deleting the contact."));
            }
        }
    }
}
