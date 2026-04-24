using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public sealed class StudentController(IStudentService _service) : BaseController
{
    [HttpGet("")]
    public async Task<IActionResult> Index() =>
        await ExecuteViewAsync(() => _service.GetStudentsAsync());

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id) =>
        await ExecuteViewAsync(() => _service.GetStudentDetailsAsync(id));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllJson() =>
        await ExecuteApiAsync(() => _service.GetStudentsAsync());

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateStudentViewModel vm) =>
        await ExecuteApiAsync(() => _service.CreateStudentAsync(vm));

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentViewModel vm) =>
        await ExecuteApiAsync(() => _service.UpdateStudentAsync(id, vm));

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id) =>
        await ExecuteApiAsync(() => _service.DeleteStudentAsync(id));
}