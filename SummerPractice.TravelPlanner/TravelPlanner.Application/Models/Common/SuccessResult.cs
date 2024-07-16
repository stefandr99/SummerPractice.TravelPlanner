namespace TravelPlanner.Application.Models.Common
{
    public class SuccessResult : Result
    {
        public SuccessResult()
        {
            this.IsSuccess = true;
        }
    }

    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T data) : base(data)
        {
            this.IsSuccess = true;
        }
    }
}
