namespace TravelPlanner.API.Validators.User
{
    using Application.Models.User.Requests;
    using FluentValidation;

    public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegisterRequestModelValidator()
        {
            this.ClassLevelCascadeMode = CascadeMode.Continue;

            this.RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Length(5, 20).WithMessage("Username must be between 5 and 20 characters.")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Username can only contain letters and numbers.");

            this.RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            this.RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Must(this.BeAValidDomain).WithMessage("Email domain is not allowed.");
        }

        private bool BeAValidDomain(string email)
        {
            var allowedDomains = new[] { "gmail.com", "centric.eu" };
            var emailDomain = email.Split('@').Last();

            return allowedDomains.Contains(emailDomain);
        }
    }
}
