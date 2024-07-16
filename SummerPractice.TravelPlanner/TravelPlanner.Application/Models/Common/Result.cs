namespace TravelPlanner.Application.Models.Common;

public abstract class Result
{
    public bool IsSuccess { get; protected set; }

    public string Message { get; protected set; }
}

public abstract class Result<T>(T data) : Result
{
    public T Data { get; protected set; } = data;
}