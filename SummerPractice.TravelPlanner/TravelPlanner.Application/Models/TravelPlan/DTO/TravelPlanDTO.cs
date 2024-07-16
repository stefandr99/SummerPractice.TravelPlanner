namespace TravelPlanner.Application.Models.TravelPlan.DTO
{
    public class TravelPlanDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public DateTime StartDate { get; set; }

        public int DurationInDays { get; set; }

        public IEnumerable<ActivityDTO> Activities { get; set; }

        public string AuthorName { get; set; }
    }
}
