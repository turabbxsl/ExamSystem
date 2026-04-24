public interface ICourseService
{

    Task<Result<CourseListViewModel>> GetCoursesAsync();
    Task<Result<CourseViewModel?>> GetCourseDetailsAsync(int id);
    Task<Result<bool>> CreateCourseAsync(CreateCourseViewModel vm);
    Task<Result<bool>> UpdateCourseAsync(int id, UpdateCourseViewModel vm);
    Task<Result<bool>> DeleteCourseAsync(int id);
}

public sealed class CourseService(CourseApiClient apiClient) : ICourseService
{
    public async Task<Result<CourseListViewModel>> GetCoursesAsync()
    {
        var response = await apiClient.GetAllAsync();
        if (!response.IsSuccess)
            return Result<CourseListViewModel>.Failure(response.Error ?? "Course loading failed.");

        var viewModel = new CourseListViewModel
        {
            Courses = response.Value?.Select(d => d.ToViewModel()).ToList() ?? []
        };
        return Result<CourseListViewModel>.Success(viewModel);
    }

    public async Task<Result<CourseViewModel?>> GetCourseDetailsAsync(int id)
    {
        var response = await apiClient.GetByIdAsync(id);
        if (!response.IsSuccess)
            return Result<CourseViewModel?>.Failure(response.Error ?? "Course not found.");

        return Result<CourseViewModel?>.Success(response.Value?.ToViewModel());
    }

    public async Task<Result<bool>> CreateCourseAsync(CreateCourseViewModel vm)
    {
        var response = await apiClient.CreateAsync(vm.ToDto());
        return response.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(response.Error ?? "Course creation failed.");
    }

    public async Task<Result<bool>> UpdateCourseAsync(int id, UpdateCourseViewModel vm)
    {
        var response = await apiClient.UpdateAsync(id, vm.ToDto());
        return response.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(response.Error ?? "Course update failed.");
    }

    public async Task<Result<bool>> DeleteCourseAsync(int id)
    {
        var response = await apiClient.DeleteAsync(id);
        return response.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(response.Error ?? "Course deletion failed.");
    }
}