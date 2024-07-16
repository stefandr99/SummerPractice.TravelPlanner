namespace TravelPlanner.Validators.Tests
{
    using API.Validators.User;
    using Application.Models.User.Requests;
    using FluentValidation.TestHelper;

    public class RegisterRequestModelValidatorTests
    {
        private readonly RegisterRequestModelValidator _validator = new();

        [Fact]
        public void Should_HaveError_When_UsernameIsEmpty()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Username = string.Empty;

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Does_Not_Contain_Lowercase()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Password = "PASSWORD1!";

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_HaveError_When_EmailIsInvalid()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Email = "invalid-email";

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        private RegisterRequestModel GetValidModel()
        {
            return new RegisterRequestModel { Username = "ValidUsername", Password = "ValidPass1!", Email = "user@centric.eu" };
        }
    }
}
