using System.Net;

public class CourseApiClient(HttpClient http, ILogger<CourseApiClient> logger)
{
    private const string Base = "api/courses";

    public async Task<Result<IEnumerable<CourseDto>>> GetAllAsync()
    {
        try
        {
            var response = await http.GetAsync(Base);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                return Result<IEnumerable<CourseDto>>.Failure(
                    $"API Error: {response.ReasonPhrase}", (int)response.StatusCode);
            }

            var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = await response.Content.ReadFromJsonAsync<Result<IEnumerable<CourseDto>>>(options);
            return result ?? Result<IEnumerable<CourseDto>>.Failure("Empty response", 500);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "GetAll courses error");
            return Result<IEnumerable<CourseDto>>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }

    public async Task<Result<CourseDto>> GetByIdAsync(int id)
    {
        try
        {
            var response = await http.GetAsync($"{Base}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return Result<CourseDto>.Failure("Course not found", 404);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<CourseDto>();
            return Result<CourseDto>.Success(data!);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "GetById course error. Id:{Id}", id);
            return Result<CourseDto>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }

    public async Task<Result<CourseDto>> CreateAsync(CreateCourseDto dto)
    {
        try
        {
            var response = await http.PostAsJsonAsync(Base, dto);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<CourseDto>();
            return Result<CourseDto>.Success(data!, 201);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Create course error");
            return Result<CourseDto>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }

    public async Task<Result<CourseDto>> UpdateAsync(int id, UpdateCourseDto dto)
    {
        try
        {
            var response = await http.PutAsJsonAsync($"{Base}/{id}", dto);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return Result<CourseDto>.Failure("Course not found", 404);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<CourseDto>();
            return Result<CourseDto>.Success(data!);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Update course error. Id:{Id}", id);
            return Result<CourseDto>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        try
        {
            var response = await http.DeleteAsync($"{Base}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return Result<bool>.Failure("Course not found", 404);

            response.EnsureSuccessStatusCode();
            return Result<bool>.Success(true);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Delete course error. Id:{Id}", id);
            return Result<bool>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }
}