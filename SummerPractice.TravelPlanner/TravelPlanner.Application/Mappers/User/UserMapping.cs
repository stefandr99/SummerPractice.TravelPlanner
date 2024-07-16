namespace TravelPlanner.Application.Mappers.User
{
    using Domain.Entities;
    using Models.User.DTO;

    public static class UserMapping
    {
        public static UserDTO ToUserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
