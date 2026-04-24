public interface IExamService
{
    Task<Result<ExamListViewModel>> GetExamsAsync();
    Task<Result<ExamViewModel?>> GetExamDetailsAsync(int id);
    Task<Result<bool>> CreateExamAsync(CreateExamViewModel vm);
    Task<Result<bool>> UpdateExamAsync(int id, UpdateExamViewModel vm);
    Task<Result<bool>> DeleteExamAsync(int id);
}

public sealed class ExamService(ExamApiClient apiClient) : IExamService
{
    public async Task<Result<ExamListViewModel>> GetExamsAsync()
    {
        var response = await apiClient.GetAllAsync();

        if (!response.IsSuccess)
            return Result<ExamListViewModel>.Failure(response.Error ?? "Exam loading failed.");

        var viewModel = new ExamListViewModel
        {
            Exams = response.Value?.Select(d => d.ToViewModel()).ToList() ?? []
        };

        return Result<ExamListViewModel>.Success(viewModel);
    }

    public async Task<Result<ExamViewModel?>> GetExamDetailsAsync(int id)
    {
        var response = await apiClient.GetByIdAsync(id);

        if (!response.IsSuccess)
            return Result<ExamViewModel?>.Failure(response.Error ?? "Exam not found.");

        return Result<ExamViewModel?>.Success(response.Value?.ToViewModel());
    }

    public async Task<Result<bool>> CreateExamAsync(CreateExamViewModel vm)
    {
        var response = await apiClient.CreateAsync(vm.ToDto());
        return response.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(response.Error ?? "Error happened");
    }

    public async Task<Result<bool>> UpdateExamAsync(int id, UpdateExamViewModel vm)
    {
        var response = await apiClient.UpdateAsync(id, vm.ToUpdateDto());
        return response.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(response.Error ?? "Error happened");
    }

    public async Task<Result<bool>> DeleteExamAsync(int id)
    {
        var response = await apiClient.DeleteAsync(id);
        return response.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(response.Error ?? "Error happened");
    }
}