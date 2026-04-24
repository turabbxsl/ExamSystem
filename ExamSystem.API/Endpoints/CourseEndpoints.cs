
namespace ExamSystem.API.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/courses").WithTags("Courses");

        group.MapGet("/", GetCourses);
        group.MapGet("/{id:int}", GetCourseById);
        group.MapPost("/", CreateCourse);
        group.MapPut("/{id:int}", UpdateCourse);
        group.MapDelete("/{id:int}", DeleteCourse);
    }

    private static async Task<IResult> GetCourses(ICourseService service)
        => (await service.GetAllAsync()).ToHttpResult();

    private static async Task<IResult> GetCourseById(int id, ICourseService service)
        => (await service.GetByIdAsync(id)).ToHttpResult();

    private static async Task<IResult> CreateCourse(CreateCourseDto dto, ICourseService service)
        => (await service.CreateAsync(dto)).ToHttpResult();

    private static async Task<IResult> UpdateCourse(int id, UpdateCourseDto dto, ICourseService service)
        => (await service.UpdateAsync(id, dto)).ToHttpResult();

    private static async Task<IResult> DeleteCourse(int id, ICourseService service)
        => (await service.DeleteAsync(id)).ToHttpResult();
}