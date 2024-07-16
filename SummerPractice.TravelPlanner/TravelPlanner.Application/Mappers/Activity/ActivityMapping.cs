namespace TravelPlanner.Application.Mappers.Activity
{
    using Domain.Entities;
    using Models.TravelPlan.DTO;
    using Models.TravelPlan.Requests;

    public static class ActivityMapping
    {
        public static ActivityDTO ToActivityDTO(Activity activity)
        {
            return new()
            {
                Id = activity.Id,
                Name = activity.Name,
                Day = activity.Day,
                Duration = activity.Duration
            };
        }

        public static Activity RequestModelToEntity(ActivityRequestModel requestModel)
        {
            return new()
            {
                Name = requestModel.Name,
                Day = requestModel.Day,
                Duration = requestModel.Duration
            };
        }
    }
}
