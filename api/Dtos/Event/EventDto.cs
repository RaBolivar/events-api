namespace api.Dtos.Event
{
  public class EventDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
  }
}