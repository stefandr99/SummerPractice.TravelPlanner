namespace TravelPlanner.Application.Models.TravelPlan.DTO
{
    using Common;

    public class TravelPlanPreviewDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public int DurationInDays { get; set; }

        public string AuthorName { get; set; }
    }
}
