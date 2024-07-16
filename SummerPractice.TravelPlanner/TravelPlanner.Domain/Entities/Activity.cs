namespace TravelPlanner.Domain.Entities;

public class Activity
{
    public int Id { get; set; }

    public int Day { get; set; }

    public string Name { get; set; }

    public int Duration { get; set; }

    public int TravelPlanId { get; set; }

    public TravelPlan TravelPlan { get; set; }
}