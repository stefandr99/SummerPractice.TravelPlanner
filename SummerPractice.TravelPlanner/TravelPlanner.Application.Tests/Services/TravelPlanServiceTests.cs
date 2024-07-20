namespace TravelPlanner.Application.Tests.Services;

using Application.Services;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Models.TravelPlan.Requests;
using NSubstitute;
using System;
using NSubstitute.ReturnsExtensions;

public class TravelPlanServiceTests
{
    private readonly ITravelPlanRepository _travelPlanRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITravelPlanService _travelPlanService;

    public TravelPlanServiceTests()
    {
        this._travelPlanRepository = Substitute.For<ITravelPlanRepository>();
        this._userRepository = Substitute.For<IUserRepository>();
        this._travelPlanService = new TravelPlanService(this._travelPlanRepository, this._userRepository);
    }

    [Fact]
    public async Task GetTravelPlanByIdAsync_Should_ReturnMappedTravelPlan_When_RepositoryReturnsValidTravelPlan()
    {
        // Arrange
        var travelPlanId = 1;
        this._travelPlanRepository.GetTravelPlanByIdAsync(travelPlanId).Returns(new TravelPlan
        {
            Id = 1,
            Name = "Iasi travel plan",
            Country = "Romania"
        });

        // Act
        var result = await this._travelPlanService.GetTravelPlanByIdAsync(travelPlanId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Id.Should().Be(travelPlanId);
        result.Data.Country.Should().Be("Romania");
    }

    [Fact]
    public async Task UpdateTravelPlanAsync_Should_ThrowException_When_PlanThatHasToBeUpdatedDoesNotExist()
    {
        // Arrange
        var userId = 1;
        var requestModel = new UpdateTravelPlanRequestModel
        {
            Id = 2,
            Name = "Test Plan",
            DurationInDays = 3,
            StartDate = DateTime.Now,
            Activities = new()
            {
                new ActivityRequestModel
                {
                    Day = 1,
                    Duration = 4,
                    Name = "Activity 1"
                }
            }
        };

        this._travelPlanRepository.GetTravelPlanByIdAsync(2).ReturnsNull();

        // Act
        Func<Task> act = async () => await this._travelPlanService.UpdateTravelPlanAsync(userId, requestModel);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Travel plan not found");
    }
}