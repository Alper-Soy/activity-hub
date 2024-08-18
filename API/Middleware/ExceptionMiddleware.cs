using System.Net;
using System.Text.Json;
using Application.Core;

namespace API.Middleware;

public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger,
    IHostEnvironment env)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
                ? new AppException(context.Response.StatusCode, ex.Message, ex.StackTrace)
                : new AppException(context.Response.StatusCode, "Internal Server Error");
            

            var json = JsonSerializer.Serialize(response, JsonOptions);

            await context.Response.WriteAsync(json);
        }
    }
}
