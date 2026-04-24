public static class ResultExtensions
{
    public static IResult ToHttpResult<T>(this Result<T> result)
    {
        return result.IsSuccess switch
        {
            true => result.StatusCode switch
            {
                201 => Results.Created(string.Empty, result),
                _ => Results.Ok(result)
            },
            false => result.StatusCode switch
            {
                404 => Results.NotFound(result),
                400 => Results.BadRequest(result),
                _ => Results.Problem(result.Error)
            }
        };
    }
}