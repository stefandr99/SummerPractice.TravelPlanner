namespace TravelPlanner.Application.Models.TravelPlan.Requests
{
    public class CreateTravelPlanRequestModel : TravelPlanBaseRequestModel
    {
        public string Country { get; set; }

        public string City { get; set; }
    }
}
