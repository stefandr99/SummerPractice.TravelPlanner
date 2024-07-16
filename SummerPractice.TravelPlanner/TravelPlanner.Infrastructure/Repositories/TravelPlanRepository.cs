namespace TravelPlanner.Infrastructure.Repositories
{
    using Data;
    using Domain.Common.Exceptions;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TravelRepository(ApplicationDbContext context) : ITravelPlanRepository
    {
        public async Task<TravelPlan> GetTravelPlanByIdAsync(int id)
        {
            return await context.TravelPlans.Include(tp => tp.Activities).FirstOrDefaultAsync(tp => tp.Id == id);
        }

        public async Task<IEnumerable<TravelPlan>> GetAllTravelPlansAsync()
        {
            return await context.TravelPlans.ToListAsync();
        }

        public async Task<IEnumerable<TravelPlan>> GetTravelPlansByUserIdAsync(int userId)
        {
            return await context.TravelPlans.Where(tp => tp.UserId == userId).ToListAsync();
        }

        public async Task AddTravelPlanAsync(TravelPlan travelPlan)
        {
            await context.TravelPlans.AddAsync(travelPlan);
        }

        public async Task UpdateTravelPlanAsync(TravelPlan travelPlan)
        {
            context.TravelPlans.Update(travelPlan);
        }

        public async Task DeleteTravelPlanAsync(int id)
        {
            var travelPlan = await context.TravelPlans.FindAsync(id);

            if (travelPlan == null)
            {
                throw new NotFoundException("Travel plan not found");
            }
            
            context.TravelPlans.Remove(travelPlan);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

    }
}
