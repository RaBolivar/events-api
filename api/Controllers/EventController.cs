using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Event;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
  [Route("api/event")]
  [ApiController]
  public class EventController : ControllerBase
  {
    private readonly ApplicationDBContext _context;
    public EventController(ApplicationDBContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var events = await _context.Events.ToListAsync();
      var eventsDto = events.Select(events => events.ToDto());
      return Ok(eventsDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getById([FromRoute] int id)
    {
      var _event = await _context.Events.FirstOrDefaultAsync(u => u.Id == id);
      if (_event == null)
      {
        return NotFound();
      }
      return Ok(_event.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventRequestDto eventDto)
    {
      var eventModel = eventDto.ToEventFromCreateDto();
      await _context.Events.AddAsync(eventModel);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(getById), new { id = eventModel.Id }, eventModel.ToDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateEventRequestDto eventDto)
    {
      var eventModel = await _context.Events.FirstOrDefaultAsync(_event => _event.Id == id);
      if (eventModel == null)
      {
        return NotFound();
      }
      eventModel.Name = eventDto.Name;
      eventModel.Description = eventDto.Description;
      eventModel.Location = eventDto.Location;
      eventModel.Date = eventDto.Date;

      await _context.SaveChangesAsync();

      return Ok(eventModel.ToDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      var eventModel = await _context.Events.FirstOrDefaultAsync(_event => _event.Id == id);
      if (eventModel == null)
      {
        return NotFound();
      }
      _context.Events.Remove(eventModel);

      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
