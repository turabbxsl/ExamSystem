using ExamSystem.Web.ViewModels;

namespace ExamSystem.Web.Services
{
    public interface IDashboardService
    {
        Task<Result<DashboardViewModel>> GetStatisticsAsync();
    }

    public sealed class DashboardService(
            IStudentService studentService,
            ICourseService courseService,
            IExamService examService) : IDashboardService
    {

        public async Task<Result<DashboardViewModel>> GetStatisticsAsync()
        {
            try
            {
                var studentTask = studentService.GetStudentsAsync();
                var courseTask = courseService.GetCoursesAsync();
                var examTask = examService.GetExamsAsync();

                await Task.WhenAll(studentTask, courseTask, examTask);

                var stats = new DashboardViewModel(
                    studentTask.Result.Value?.Students?.Count() ?? 0,
                    courseTask.Result.Value?.Courses?.Count() ?? 0,
                    examTask.Result.Value?.Exams?.Count() ?? 0
                );

                return Result<DashboardViewModel>.Success(stats);
            }
            catch (Exception ex)
            {
                return Result<DashboardViewModel>.Failure(["Statisctics data error"]);
            }
        }
    }
}
