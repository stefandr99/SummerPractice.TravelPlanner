namespace TravelPlanner.Domain.Entities;

public class TravelPlan
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public int DurationInDays { get; set; }

    public IEnumerable<Activity> Activities { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public DateTime StartDate { get; set; }
}