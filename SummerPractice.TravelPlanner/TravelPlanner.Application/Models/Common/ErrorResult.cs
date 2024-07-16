namespace TravelPlanner.Application.Models.Common
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message)
        {
            this.IsSuccess = false;
            this.Message = message;
        }
    }
}
