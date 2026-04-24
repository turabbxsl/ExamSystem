using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public sealed class ExamController(IExamService _service) : BaseController
{

    [HttpGet("")]
    public async Task<IActionResult> Index() =>
        await ExecuteViewAsync(() => _service.GetExamsAsync());

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id) =>
        await ExecuteViewAsync(() => _service.GetExamDetailsAsync(id));


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllJson() =>
        await ExecuteApiAsync(() => _service.GetExamsAsync());

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateExamViewModel vm) =>
        await ExecuteApiAsync(() => _service.CreateExamAsync(vm));

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateExamViewModel vm) =>
        await ExecuteApiAsync(() => _service.UpdateExamAsync(id, vm));

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id) =>
        await ExecuteApiAsync(() => _service.DeleteExamAsync(id));
}