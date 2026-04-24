namespace ExamSystem.Web.Options;

public sealed class ApiOptions
{
    public const string SectionName = "Api";

    public string BaseUrl { get; init; } = string.Empty;
    public int TimeoutSeconds { get; init; } = 30;
    public int RetryCount { get; init; } = 3;
    public int RetryDelaySeconds { get; init; } = 2;
}