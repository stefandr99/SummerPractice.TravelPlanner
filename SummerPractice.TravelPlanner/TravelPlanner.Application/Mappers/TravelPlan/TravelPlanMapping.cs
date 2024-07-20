namespace TravelPlanner.Application.Mappers.TravelPlan
{
    using Activity;
    using Domain.Entities;
    using Models.TravelPlan.DTO;
    using Models.TravelPlan.Requests;

    public static class TravelPlanMapping
    {
        public static TravelPlanDTO ToTravelPlanDTO(TravelPlan travelPlan)
        {
            return new()
            {
                Id = travelPlan.Id,
                Name = travelPlan.Name,
                Country = travelPlan.Country,
                //City = travelPlan.City,
                //DurationInDays = travelPlan.DurationInDays,
                //StartDate = travelPlan.StartDate,
                //AuthorName = travelPlan.User.Username,
                //Activities = travelPlan.Activities.Select(ActivityMapping.ToActivityDTO)
            };
        }

        public static TravelPlanPreviewDTO ToTravelPlanPreviewDTO(TravelPlan travelPlan)
        {
            return new()
            {
                Id = travelPlan.Id,
                Name = travelPlan.Name,
                Country = travelPlan.Country,
                DurationInDays = travelPlan.DurationInDays,
                AuthorName = travelPlan.User.Username,
            };
        }

        public static TravelPlan CreateModelToEntity(CreateTravelPlanRequestModel requestModel)
        {
            return new()
            {
                Name = requestModel.Name,
                Country = requestModel.Country,
                City = requestModel.City,
                DurationInDays = requestModel.DurationInDays,
                StartDate = requestModel.StartDate,
                Activities = requestModel.Activities.Select(ActivityMapping.RequestModelToEntity)
            };
        }

        public static TravelPlan UpdateModelToEntity(UpdateTravelPlanRequestModel requestModel)
        {
            return new()
            {
                Name = requestModel.Name,
                DurationInDays = requestModel.DurationInDays,
                StartDate = requestModel.StartDate,
                Activities = requestModel.Activities.Select(ActivityMapping.RequestModelToEntity)
            };
        }
    }
}
