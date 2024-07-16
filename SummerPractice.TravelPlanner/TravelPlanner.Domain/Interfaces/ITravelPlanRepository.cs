namespace TravelPlanner.Domain.Interfaces
{
    using Entities;

    public interface ITravelPlanRepository
    {
        Task<TravelPlan> GetTravelPlanByIdAsync(int id);

        Task<IEnumerable<TravelPlan>> GetAllTravelPlansAsync();

        Task<IEnumerable<TravelPlan>> GetTravelPlansByUserIdAsync(int userId);

        Task AddTravelPlanAsync(TravelPlan travelPlan);

        Task UpdateTravelPlanAsync(TravelPlan travelPlan);

        Task DeleteTravelPlanAsync(int id);

        Task SaveAsync();
    }

}
