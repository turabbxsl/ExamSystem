
namespace ExamSystem.API.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/students").WithTags("Students");

        group.MapGet("/", GetStudents);
        group.MapGet("/{id:int}", GetStudentById);
        group.MapPost("/", CreateStudent);
        group.MapPut("/{id:int}", UpdateStudent);
        group.MapDelete("/{id:int}", DeleteStudent);
    }

    private static async Task<IResult> GetStudents(IStudentService service)
        => (await service.GetAllAsync()).ToHttpResult();

    private static async Task<IResult> GetStudentById(int id, IStudentService service)
        => (await service.GetByIdAsync(id)).ToHttpResult();

    private static async Task<IResult> CreateStudent(CreateStudentDto dto, IStudentService service)
        => (await service.CreateAsync(dto)).ToHttpResult();

    private static async Task<IResult> UpdateStudent(int id, UpdateStudentDto dto, IStudentService service)
        => (await service.UpdateAsync(id, dto)).ToHttpResult();

    private static async Task<IResult> DeleteStudent(int id, IStudentService service)
        => (await service.DeleteAsync(id)).ToHttpResult();
}