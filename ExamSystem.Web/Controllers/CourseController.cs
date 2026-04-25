using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public sealed class CourseController(ICourseService _service) : BaseController
{

    [HttpGet("")]
    public async Task<IActionResult> Index() => await ExecuteViewAsync(() => _service.GetCoursesAsync());


    [HttpGet("get-all")]
    public async Task<IActionResult> GetCoursesJson() =>
        await ExecuteApiAsync(() => _service.GetCoursesAsync());


    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCourseViewModel vm) =>
        await ExecuteApiAsync(() => _service.CreateCourseAsync(vm));


    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseViewModel vm) =>
    await ExecuteApiAsync(() => _service.UpdateCourseAsync(id, vm));


    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id) =>
        await ExecuteApiAsync(() => _service.DeleteCourseAsync(id));
}