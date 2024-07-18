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
    public class ErrorResult<T> : Result<T>
    {
        public ErrorResult(T data) : base(default)
        {
            this.IsSuccess = false;
        }
    }
}
