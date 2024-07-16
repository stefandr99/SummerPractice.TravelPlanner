namespace TravelPlanner.Application.Models.TravelPlan.Requests
{
    public abstract class TravelPlanBaseRequestModel
    {
        public string Name { get; set; }

        public int DurationInDays { get; set; }

        public DateTime StartDate { get; set; }

        public List<ActivityRequestModel> Activities { get; set; }
    }
}
