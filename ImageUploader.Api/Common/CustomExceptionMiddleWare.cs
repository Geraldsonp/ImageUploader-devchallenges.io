namespace ImageUploader.Api.Common;

public class CustomExceptionMiddleWare
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
           await _next(httpContext);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await HandleException(httpContext, e);
        }
    }

    private async Task HandleException(HttpContext context, Exception e)
    {
        var response = context.Response;

        Console.WriteLine(e.Message);

        if (!response.HasStarted)
        {
            response.ContentType = "application/json";
            response.StatusCode = StatusCodes.Status500InternalServerError;
            await response.WriteAsync("There was an error please check the logs");
        }
    }
}