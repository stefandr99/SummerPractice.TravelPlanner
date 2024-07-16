namespace TravelPlanner.Validators.Tests
{
    using API.Validators.User;
    using Application.Models.User.Requests;
    using FluentValidation.TestHelper;

    public class RegisterRequestModelValidatorTests
    {
        private readonly RegisterRequestModelValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Username_Is_Empty()
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
        public void Should_Have_Error_When_Username_Is_Short()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Username = "abc";

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username);
        }

        [Fact]
        public void Should_Have_Error_When_Username_Is_Long()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Username = new string('a', 21);

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username);
        }

        [Fact]
        public void Should_Have_Error_When_Username_Has_Special_Characters()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Username = "abc@123";

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Username);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Password = string.Empty;

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Short()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Password = "Pass1!";

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Does_Not_Contain_Uppercase()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Password = "password1!";

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
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
        public void Should_Have_Error_When_Password_Does_Not_Contain_Number()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Password = "Password!";

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Does_Not_Contain_Special_Character()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Password = "Password1";

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Email = string.Empty;

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Email = "invalid-email";

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Domain_Is_Not_Allowed()
        {
            // Arrange
            var model = this.GetValidModel();
            model.Email = "user@notallowed.com";

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Have_Error_Whenmodel_Is_Valid()
        {
            // Arrange
            var model = this.GetValidModel();

            // Act
            var result = this._validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        private RegisterRequestModel GetValidModel()
        {
            return new RegisterRequestModel { Username = "ValidUsername", Password = "ValidPass1!", Email = "user@centric.eu" };
        }
    }
}
