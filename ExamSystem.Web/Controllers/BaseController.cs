using Microsoft.AspNetCore.Mvc;

public abstract class BaseController : Controller
{

    // json response format for api calls
    protected async Task<IActionResult> ExecuteApiAsync<T>(Func<Task<Result<T>>> action)
    {
        try
        {
            var result = await action();

            if (!result.IsSuccess)
                return Json(new { isSuccess = false, data = (object?)null, errors = new List<string> { result.Error } });

            return Json(new { isSuccess = true, data = result, errors = (List<string>?)null });
        }
        catch (Exception ex)
        {
            return Json(new { isSuccess = false, data = (object?)null, errors = new List<string> { ex.Message } });
        }
    }


    // view response format for regular page requests
    protected async Task<IActionResult> ExecuteViewAsync<T>(Func<Task<T>> action, string viewName = "")
    {
        try
        {
            var result = await action();
            return string.IsNullOrEmpty(viewName) ? View(result) : View(viewName, result);
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Daxili xəta baş verdi: " + ex.Message;
            return View("Error"); 
        }
    }
}