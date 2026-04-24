using System.Net;

public sealed class StudentApiClient(HttpClient http, ILogger<StudentApiClient> logger)
{
    private const string Base = "api/students";

    public async Task<Result<IEnumerable<StudentDto>>> GetAllAsync()
    {
        try
        {
            var response = await http.GetAsync(Base);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                return Result<IEnumerable<StudentDto>>.Failure(
                    $"API Error: {response.ReasonPhrase}", (int)response.StatusCode);
            }

            var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = await response.Content.ReadFromJsonAsync<Result<IEnumerable<StudentDto>>>(options);

            return result ?? Result<IEnumerable<StudentDto>>.Failure("Empty response", 500);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Critical error occurred while executing GetAllAsync.");
            return Result<IEnumerable<StudentDto>>.Failure("Internal system error occurred.", 500);
        }
    }

    public async Task<Result<StudentDto>> GetByIdAsync(int id)
    {
        try
        {
            var response = await http.GetAsync($"{Base}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return Result<StudentDto>.Failure("Şagird tapılmadı", 404);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<StudentDto>();
            return Result<StudentDto>.Success(data!);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "GetById student xəta. Id:{Id}", id);
            return Result<StudentDto>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }

    public async Task<Result<StudentDto>> CreateAsync(CreateStudentDto dto)
    {
        try
        {
            var response = await http.PostAsJsonAsync(Base, dto);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<StudentDto>();
            return Result<StudentDto>.Success(data!, 201);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Create student xəta");
            return Result<StudentDto>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }

    public async Task<Result<StudentDto>> UpdateAsync(int id, UpdateStudentDto dto)
    {
        try
        {
            var response = await http.PutAsJsonAsync($"{Base}/{id}", dto);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return Result<StudentDto>.Failure("Şagird tapılmadı", 404);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<StudentDto>();
            return Result<StudentDto>.Success(data!);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Update student xəta. Id:{Id}", id);
            return Result<StudentDto>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        try
        {
            var response = await http.DeleteAsync($"{Base}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return Result<bool>.Failure("Şagird tapılmadı", 404);

            response.EnsureSuccessStatusCode();
            return Result<bool>.Success(true);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Delete student xəta. Id:{Id}", id);
            return Result<bool>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }
}