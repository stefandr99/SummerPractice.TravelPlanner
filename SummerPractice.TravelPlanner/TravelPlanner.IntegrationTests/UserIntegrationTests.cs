using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Json;
using TravelPlanner.Application.Models.Common;
using TravelPlanner.Application.Models.User.DTO;
using TravelPlanner.Application.Models.User.Requests;
using TravelPlanner.IntegrationTests.Setup;

namespace TravelPlanner.IntegrationTests
{
    using FluentAssertions;

    public class UserIntegrationTests(CustomWebApplicationFactory<Program> factory)
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();
        private const string baseUrl = "api/v1/user";
    }
}
