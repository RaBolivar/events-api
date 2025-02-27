using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Event;
using api.Mappers;
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
  }
}
