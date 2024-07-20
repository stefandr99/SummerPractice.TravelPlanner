namespace TravelPlanner.Application.Models.TravelPlan.DTO
{
    using Common;

    public class ActivityDTO : BaseDTO
    {
        public int Day { get; set; }

        public string Name { get; set; }

        public int Duration { get; set; }
    }
}
