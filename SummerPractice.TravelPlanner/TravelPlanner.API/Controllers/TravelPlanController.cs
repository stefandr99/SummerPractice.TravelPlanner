namespace TravelPlanner.API.Controllers
{
    using Application.Models.TravelPlan.Requests;
    using Application.Services.Interfaces;
    using Domain.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TravelPlanController(ITravelPlanService travelService) : BaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var travelPlan = await travelService.GetTravelPlanByIdAsync(id);

            if (!travelPlan.IsSuccess)
                return this.NotFound();

            return this.Ok(travelPlan);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var travelPlans = await travelService.GetAllTravelPlansAsync();

            return this.Ok(travelPlans.Data.Select(tp => new
            {
                tp.Id,
                tp.Name,
                tp.Country,
                tp.DurationInDays
            }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, [FromBody] CreateTravelPlanRequestModel request)
        {
            try
            {
                await travelService.CreateTravelPlanAsync(id, request);

                return this.Created();
            }
            catch (ArgumentException)
            {
                return this.BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTravelPlanRequestModel request)
        {
            try
            {
                await travelService.UpdateTravelPlanAsync(id, request);

                return this.Ok();
            }
            catch (ArgumentException)
            {
                return this.BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await travelService.DeleteTravelPlanAsync(id);

            return this.Ok();
        }
    }
}
