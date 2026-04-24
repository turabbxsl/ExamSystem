public interface IStudentService
{
    Task<Result<StudentListViewModel>> GetStudentsAsync();
    Task<Result<StudentViewModel?>> GetStudentDetailsAsync(int id);
    Task<Result<bool>> CreateStudentAsync(CreateStudentViewModel vm);
    Task<Result<bool>> UpdateStudentAsync(int id, UpdateStudentViewModel vm);
    Task<Result<bool>> DeleteStudentAsync(int id);
}

public sealed class StudentService(StudentApiClient apiClient) : IStudentService
{
    public async Task<Result<StudentListViewModel>> GetStudentsAsync()
    {
        var response = await apiClient.GetAllAsync();
        if (!response.IsSuccess)
            return Result<StudentListViewModel>.Failure(response.Error ?? "Student loading failed.");

        var viewModel = new StudentListViewModel
        {
            Students = response.Value?.Select(d => d.ToViewModel()).ToList() ?? []
        };
        return Result<StudentListViewModel>.Success(viewModel);
    }

    public async Task<Result<StudentViewModel?>> GetStudentDetailsAsync(int id)
    {
        var response = await apiClient.GetByIdAsync(id);
        if (!response.IsSuccess)
            return Result<StudentViewModel?>.Failure(response.Error ?? "Student not found.");

        return Result<StudentViewModel?>.Success(response.Value?.ToViewModel());
    }

    public async Task<Result<bool>> CreateStudentAsync(CreateStudentViewModel vm)
    {
        var response = await apiClient.CreateAsync(vm.ToDto());
        return response.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(response.Error ?? "Student creation failed.");
    }

    public async Task<Result<bool>> UpdateStudentAsync(int id, UpdateStudentViewModel vm)
    {
        var response = await apiClient.UpdateAsync(id, vm.ToDto());
        return response.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(response.Error ?? "Student update failed.");
    }

    public async Task<Result<bool>> DeleteStudentAsync(int id)
    {
        var response = await apiClient.DeleteAsync(id);
        return response.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(response.Error ?? "Student deletion failed.");
    }
}