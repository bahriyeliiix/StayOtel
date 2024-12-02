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
            var contacts = await _mediator.Send(new GetHotelContactsQuery { HotelId = hotelId });
            if (contacts == null || contacts.Count == 0)
            {
                return NotFound(new ApiResponse<string>("No contacts found for this hotel."));
            }
            return Ok(new ApiResponse<List<HotelContactDto>>(contacts));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHotelContact([FromBody] CreateHotelContactCommand command)
        {
            if (command == null)
            {
                return BadRequest(new ApiResponse<string>("Invalid data provided."));
            }

            var contactId = await _mediator.Send(command);
            if (contactId == Guid.Empty)
            {
                return BadRequest(new ApiResponse<string>("Failed to create contact."));
            }

            return CreatedAtAction(nameof(GetHotelContacts), new { hotelId = command.HotelId }, new ApiResponse<Guid>(contactId));
        }


        [HttpDelete("delete/{contactId}")]
        public async Task<IActionResult> DeleteHotelContact(Guid contactId)
        {
            await _mediator.Send(new DeleteHotelContactCommand { ContactId = contactId });
            
            return NoContent();
        }
    }
}
