namespace TravelPlanner.Application.Services
{
    using Domain.Entities;
    using Domain.Interfaces;
    using Interfaces;
    using Mappers.TravelPlan;
    using Models.Common;
    using Models.TravelPlan.DTO;
    using Models.TravelPlan.Requests;

    public class TravelPlanService(ITravelPlanRepository travelRepository, IUserRepository userRepository)
        : ITravelPlanService
    {
        public async Task<Result<TravelPlanDTO>> GetTravelPlanByIdAsync(int id)
        {
            var travelPlan = await travelRepository.GetTravelPlanByIdAsync(id);

            var travelPlanDTO = TravelPlanMapping.ToTravelPlanDTO(travelPlan);

            return new SuccessResult<TravelPlanDTO>(travelPlanDTO);
        }

        public async Task<Result<IEnumerable<TravelPlanDTO>>> GetAllTravelPlansAsync()
        {
            var travelPlans = await travelRepository.GetAllTravelPlansAsync();

            var travelPlansDto = travelPlans.Select(TravelPlanMapping.ToTravelPlanDTO);

            return new SuccessResult<IEnumerable<TravelPlanDTO>>(travelPlansDto);
        }

        public async Task<Result<IEnumerable<TravelPlanPreviewDTO>>> GetTravelPlansPreviewAsync()
        {
            var travelPlans = await travelRepository.GetAllTravelPlansAsync();

            var previewTravelPlans = travelPlans.Select(TravelPlanMapping.ToTravelPlanPreviewDTO);

            return new SuccessResult<IEnumerable<TravelPlanPreviewDTO>>(previewTravelPlans);
        }

        public async Task<Result<IEnumerable<TravelPlanDTO>>> GetTravelPlansByUserIdAsync(int userId)
        {
            var userTravelPlans = await travelRepository.GetTravelPlansByUserIdAsync(userId);

            var userTravelPlansDto = userTravelPlans.Select(TravelPlanMapping.ToTravelPlanDTO);

            return new SuccessResult<IEnumerable<TravelPlanDTO>>(userTravelPlansDto);
        }

        public async Task<Result> CreateTravelPlanAsync(int userId, CreateTravelPlanRequestModel requestModel)
        {
            var user = await userRepository.GetUserByIdAsync(userId);

            if (!user.IsEmailVerified)
            {
                throw new ArgumentException("User not found or email not verified");
            }

            var userPlans = await travelRepository.GetTravelPlansByUserIdAsync(userId);

            foreach (var plan in userPlans)
            {
                if (this.IsOverlapping(plan, requestModel))
                {
                    throw new ArgumentException("Travel plan conflicts with an existing plan");
                }
            }

            this.ValidateActivities(requestModel.Activities, requestModel.DurationInDays);

            var travelPlan = TravelPlanMapping.CreateModelToEntity(requestModel);

            await travelRepository.AddTravelPlanAsync(travelPlan);
            await travelRepository.SaveAsync();

            return new SuccessResult();
        }

        public async Task<Result> UpdateTravelPlanAsync(int userId, UpdateTravelPlanRequestModel requestModel)
        {
            var existingPlan = await travelRepository.GetTravelPlanByIdAsync(requestModel.Id);

            if (existingPlan == null)
            {
                throw new ArgumentException("Travel plan not found");
            }

            var userPlans = (await travelRepository.GetTravelPlansByUserIdAsync(userId))
                .Where(tp => tp.Id != requestModel.Id);

            foreach (var plan in userPlans)
            {
                if (this.IsOverlapping(plan, requestModel))
                {
                    throw new ArgumentException("Travel plan conflicts with an existing plan");
                }
            }

            this.ValidateActivities(requestModel.Activities, requestModel.DurationInDays);

            var travelPlan = TravelPlanMapping.UpdateModelToEntity(requestModel);

            await travelRepository.UpdateTravelPlanAsync(travelPlan);
            await travelRepository.SaveAsync();

            return new SuccessResult();
        }

        public async Task<Result> DeleteTravelPlanAsync(int id)
        {
            await travelRepository.DeleteTravelPlanAsync(id);
            await travelRepository.SaveAsync();

            return new SuccessResult();
        }

        private void ValidateActivities(IEnumerable<ActivityRequestModel> activities, int durationInDays)
        {
            var activityRequestModels = activities as ActivityRequestModel[] ?? activities.ToArray();

            if (activityRequestModels.Any(a => a.Day > durationInDays))
            {
                throw new ArgumentException("Activity day exceeds travel plan duration");
            }

            var activityNames = new HashSet<string>();

            foreach (var activity in activityRequestModels)
            {
                if (!activityNames.Add(activity.Name))
                {
                    throw new ArgumentException("Duplicate activity name within the same travel plan");
                }
            }
        }

        private bool IsOverlapping(TravelPlan existingPlan, TravelPlanBaseRequestModel newPlan)
        {
            var existingEndDate = existingPlan.StartDate.AddDays(existingPlan.DurationInDays);
            var newEndDate = newPlan.StartDate.AddDays(newPlan.DurationInDays);

            return existingPlan.StartDate <= newEndDate && newPlan.StartDate <= existingEndDate;
        }
    }
}
