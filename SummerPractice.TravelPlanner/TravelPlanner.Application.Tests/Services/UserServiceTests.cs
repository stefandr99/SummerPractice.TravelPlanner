namespace TravelPlanner.Application.Tests.Services
{
    using System;
    using System.Threading.Tasks;
    using Application.Services;
    using Application.Services.Interfaces;
    using Domain.Entities;
    using Domain.Interfaces;
    using FluentAssertions;
    using Models.Common;
    using Models.User.DTO;
    using NSubstitute;
    using Xunit;

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
        public async Task AuthenticateAsync_Should_ReturnSuccess_When_CredentialsAreValid()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";
            var encryptedPassword = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
            var user = new User { Username = username, Password = encryptedPassword };

            this._userRepository.GetUserByUsernameAsync(username)!.Returns(Task.FromResult(user));

            // Act
            var result = await this._userService.AuthenticateAsync(username, password);

            // Assert
            result.Should().BeOfType<SuccessResult>();
        }

        [Fact]
        public async Task AuthenticateAsync_Should_ReturnError_When_UserNotFound()
        {
            // Arrange
            var username = "nonexistentuser";
            var password = "password123";

            this._userRepository.GetUserByUsernameAsync(username)!.Returns(Task.FromResult<User>(null));

            // Act
            var result = await this._userService.AuthenticateAsync(username, password);

            // Assert
            result.Should().BeOfType<ErrorResult>();
            ((ErrorResult)result).Message.Should().Be("Authentication failed");
        }

        [Fact]
        public async Task AuthenticateAsync_Should_ReturnError_When_PasswordIsIncorrect()
        {
            // Arrange
            var username = "testuser";
            var password = "wrongpassword";
            var encryptedPassword = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("correctpassword"));
            var user = new User { Username = username, Password = encryptedPassword };

            this._userRepository.GetUserByUsernameAsync(username)!.Returns(Task.FromResult(user));

            // Act
            var result = await this._userService.AuthenticateAsync(username, password);

            // Assert
            result.Should().BeOfType<ErrorResult>();
            ((ErrorResult)result).Message.Should().Be("Authentication failed");
        }

        [Fact]
        public async Task RegisterAsync_Should_ThrowException_When_UsernameExists()
        {
            // Arrange
            var username = "existinguser";
            var password = "password123";
            var email = "test@example.com";
            var existingUser = new User { Username = username };

            this._userRepository.GetUserByUsernameAsync(username)!.Returns(Task.FromResult(existingUser));

            // Act
            Func<Task> act = async () => await this._userService.RegisterAsync(username, password, email);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Username already exists");
        }

        [Fact]
        public async Task RegisterAsync_Should_ReturnSuccess_When_RegistrationIsValid()
        {
            // Arrange
            var username = "newuser";
            var password = "password123";
            var email = "test@example.com";

            this._userRepository.GetUserByUsernameAsync(username)!.Returns(Task.FromResult<User>(null));

            // Act
            var result = await this._userService.RegisterAsync(username, password, email);

            // Assert
            result.Should().BeOfType<SuccessResult>();
            await this._userRepository.Received(1).AddUserAsync(Arg.Any<User>());
            await this._userRepository.Received(1).SaveAsync();
        }

        [Fact]
        public async Task VerifyEmailAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var userId = 1;
            var token = "valid_token";

            this._userRepository.GetUserByIdAsync(userId)!.Returns(Task.FromResult<User>(null));

            // Act
            Func<Task> act = async () => await this._userService.VerifyEmailAsync(userId, token);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("User not found");
        }

        [Fact]
        public async Task VerifyEmailAsync_Should_ReturnSuccess_When_TokenIsValid()
        {
            // Arrange
            var userId = 1;
            var token = "valid_token";
            var user = new User { Id = userId };

            this._userRepository.GetUserByIdAsync(userId)!.Returns(Task.FromResult(user));

            // Act
            var result = await this._userService.VerifyEmailAsync(userId, token);

            // Assert
            result.Should().BeOfType<SuccessResult>();
            user.IsEmailVerified.Should().BeTrue();
            await this._userRepository.Received(1).SaveAsync();
        }

        [Fact]
        public async Task VerifyEmailAsync_Should_ThrowException_When_TokenIsInvalid()
        {
            // Arrange
            var userId = 1;
            var token = "invalid_token";
            var user = new User { Id = userId };

            this._userRepository.GetUserByIdAsync(userId)!.Returns(Task.FromResult(user));

            // Act
            Func<Task> act = async () => await this._userService.VerifyEmailAsync(userId, token);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid verification token");
        }

        [Fact]
        public async Task GetUserByIdAsync_Should_ReturnMappedUser_When_UserExists()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Username = "testuser" };

            this._userRepository.GetUserByIdAsync(userId)!.Returns(Task.FromResult(user));

            // Act
            var result = await this._userService.GetUserByIdAsync(userId);

            // Assert
            result.Should().BeOfType<SuccessResult<UserDTO>>();
            result.Data.Username.Should().Be("testuser");
            result.Data.Id.Should().Be(userId);
        }

        [Fact]
        public async Task GetUserByUsernameOrEmailAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var usernameOrEmail = "testuser";
            var user = new User { Id = 1, Username = "testuser" };

            this._userRepository.GetUserByUsernameOrEmailAsync(usernameOrEmail)!.Returns(Task.FromResult(user));

            // Act
            var result = await this._userService.GetUserByUsernameOrEmailAsync(usernameOrEmail);

            // Assert
            result.Should().BeOfType<SuccessResult<UserDTO>>();
            result.Data.Username.Should().Be("testuser");
        }
    }
}
