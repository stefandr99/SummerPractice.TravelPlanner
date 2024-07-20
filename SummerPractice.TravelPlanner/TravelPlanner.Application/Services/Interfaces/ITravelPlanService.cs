namespace TravelPlanner.Application.Services.Interfaces
{
    using Domain.Entities;
    using Models.Common;
    using Models.TravelPlan.DTO;
    using Models.TravelPlan.Requests;

    public interface ITravelPlanService
    {
        Task<Result<TravelPlanDTO>> GetTravelPlanByIdAsync(int id);

        Task<Result<IEnumerable<TravelPlanDTO>>> GetAllTravelPlansAsync();

        Task<Result<IEnumerable<TravelPlanPreviewDTO>>> GetTravelPlansPreviewAsync();

        Task<Result<IEnumerable<TravelPlanDTO>>> GetTravelPlansByUserIdAsync(int userId);

        Task<Result> CreateTravelPlanAsync(int userId, CreateTravelPlanRequestModel requestModel);

        Task<Result> UpdateTravelPlanAsync(int userId, UpdateTravelPlanRequestModel requestModel);

        Task<Result> DeleteTravelPlanAsync(int id);
    }

}
