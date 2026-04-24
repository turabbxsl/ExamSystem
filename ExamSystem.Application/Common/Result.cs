using System.Text.Json.Serialization;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Value { get; private set; }
    public string Error { get; private set; } = string.Empty;
    public int StatusCode { get; private set; }

    private Result() { }

    [JsonConstructor]
    public Result(bool isSuccess, T value, string error, int statusCode)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        StatusCode = statusCode;
    }

    public static Result<T> Success(T value, int statusCode = 200) =>
        new() { IsSuccess = true, Value = value, StatusCode = statusCode };

    public static Result<T> Failure(string error, int statusCode = 400) =>
        new() { IsSuccess = false, Error = error, StatusCode = statusCode };

    public static Result<T> Failure(IEnumerable<string> errors, int statusCode = 400)
    => new() { IsSuccess = false, Error = errors.FirstOrDefault() ?? "Unknown error", StatusCode = statusCode };

    public static Result<T> NotFound(string error = "Not Found") =>
        Failure(error, 404);
}