namespace TravelPlanner.Application.Models.TravelPlan.DTO
{
    using Common;

    public class TravelPlanDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public DateTime StartDate { get; set; }

        public int DurationInDays { get; set; }

        public IEnumerable<ActivityDTO> Activities { get; set; }

        public string AuthorName { get; set; }
    }
}
