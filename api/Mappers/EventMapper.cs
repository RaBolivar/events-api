using api.Dtos.Event;
using api.Models;

namespace api.Mappers
{
  public static class EventMapper
  {
    public static EventDto ToDto(this Event eventItem)
    {
      return new EventDto
      {
        Id = eventItem.Id,
        Name = eventItem.Name,
        Location = eventItem.Location,
        Description = eventItem.Description,
        Date = eventItem.Date
      };
    }
  }
}