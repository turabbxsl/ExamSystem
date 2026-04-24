using System.Net;

public sealed class ExamApiClient(HttpClient http, ILogger<ExamApiClient> logger)
{
    private const string Base = "api/exams";

    public async Task<Result<IEnumerable<ExamDto>>> GetAllAsync()
    {
        try
        {
            var response = await http.GetAsync(Base);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                return Result<IEnumerable<ExamDto>>.Failure(
                    $"API Error: {response.ReasonPhrase}", (int)response.StatusCode);
            }

            var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = await response.Content.ReadFromJsonAsync<Result<IEnumerable<ExamDto>>>(options);

            return result ?? Result<IEnumerable<ExamDto>>.Failure("Empty response", 500);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "GetAll exams error");
            return Result<IEnumerable<ExamDto>>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }

    public async Task<Result<ExamDto>> GetByIdAsync(int id)
    {
        try
        {
            var response = await http.GetAsync($"{Base}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return Result<ExamDto>.Failure("İmtahan tapılmadı", 404);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<ExamDto>();
            return Result<ExamDto>.Success(data!);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "GetById exam error. Id:{Id}", id);
            return Result<ExamDto>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }

    public async Task<Result<ExamDto>> CreateAsync(CreateExamDto dto)
    {
        try
        {
            var response = await http.PostAsJsonAsync(Base, dto);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<ExamDto>();
            return Result<ExamDto>.Success(data!, 201);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Create exam error");
            return Result<ExamDto>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }

    public async Task<Result<ExamDto>> UpdateAsync(int id, UpdateExamDto dto)
    {
        try
        {
            var response = await http.PutAsJsonAsync($"{Base}/{id}", dto);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return Result<ExamDto>.Failure("İmtahan tapılmadı", 404);

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<ExamDto>();
            return Result<ExamDto>.Success(data!);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Update exam error. Id:{Id}", id);
            return Result<ExamDto>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        try
        {
            var response = await http.DeleteAsync($"{Base}/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return Result<bool>.Failure("İmtahan tapılmadı", 404);

            response.EnsureSuccessStatusCode();
            return Result<bool>.Success(true);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Delete exam error. Id:{Id}", id);
            return Result<bool>.Failure(ex.Message, (int)(ex.StatusCode ?? HttpStatusCode.ServiceUnavailable));
        }
    }
}