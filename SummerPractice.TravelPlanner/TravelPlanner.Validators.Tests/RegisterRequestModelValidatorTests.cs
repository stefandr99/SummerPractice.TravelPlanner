namespace TravelPlanner.Validators.Tests
{
    using API.Validators.User;
    using Application.Models.User.Requests;

    public class RegisterRequestModelValidatorTests
    {
        private readonly RegisterRequestModelValidator _validator = new();

        private RegisterRequestModel GetValidModel()
        {
            return new RegisterRequestModel { Username = "ValidUsername", Password = "ValidPass1!", Email = "user@centric.eu" };
        }
    }
}
