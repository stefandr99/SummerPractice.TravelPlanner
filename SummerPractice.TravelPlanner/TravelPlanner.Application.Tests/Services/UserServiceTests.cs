namespace TravelPlanner.Application.Tests.Services
{
    using Application.Services;
    using Application.Services.Interfaces;
    using Domain.Entities;
    using Domain.Interfaces;
    using FluentAssertions;
    using NSubstitute;

    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            this._userRepository = Substitute.For<IUserRepository>();
            this._userService = new UserService(this._userRepository);
        }

        [Fact]
        public async Task GetUserByIdAsync_Should_ReturnMappedUser_When_UserExists()
        {
            // Arrange
            var userId = 1;
            this._userRepository.GetUserByIdAsync(2).Returns(new User()
            {
                Id = 2,
                Username = "user123",
                Email = "test@centric.eu"
            });

            // Act
            var result = await this._userService.GetUserByIdAsync(userId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Id.Should().Be(1);
        }

        [Fact]
        public async Task GetUserByIdAsync_Should_CallRepositoryMethodOnce_When_GetUserById()
        {
            // Arrange
            var userId = 1;

            // Act
            await this._userService.GetUserByIdAsync(userId);

            // Assert
            await this._userRepository.Received(1).GetUserByIdAsync(userId);
            await this._userRepository.Received(0).GetUserByUsernameAsync(Arg.Any<string>());
        }

        [Fact]
        public async Task RegisterAsync_Should_ThrowException_When_UserWithThatUsernameAlreadyExists()
        {
            // Arrange
            var username = "user123";
            var password = "123456";
            var email = "test123@centric.eu";
            this._userRepository.GetUserByUsernameAsync(username).Returns(new User());

            // Act
            Func<Task> act = async () => await this._userService.RegisterAsync(username, password, email);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Username already exists");
        }
    }
}
