
namespace ExamSystem.API.Endpoints;

public static class ExamEndpoints
{
    public static void MapExamEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/exams").WithTags("Exams");

        group.MapGet("/", GetExams);
        group.MapGet("/{id:int}", GetExamById);
        group.MapPost("/", CreateExam);
        group.MapPut("/{id:int}", UpdateExam);
        group.MapDelete("/{id:int}", DeleteExam);
    }

    private static async Task<IResult> GetExams(IExamService service)
        => (await service.GetAllAsync()).ToHttpResult();

    private static async Task<IResult> GetExamById(int id, IExamService service)
        => (await service.GetByIdAsync(id)).ToHttpResult();

    private static async Task<IResult> CreateExam(CreateExamDto dto, IExamService service)
        => (await service.CreateAsync(dto)).ToHttpResult();

    private static async Task<IResult> UpdateExam(int id, UpdateExamDto dto, IExamService service)
        => (await service.UpdateAsync(id, dto)).ToHttpResult();

    private static async Task<IResult> DeleteExam(int id, IExamService service)
        => (await service.DeleteAsync(id)).ToHttpResult();
}