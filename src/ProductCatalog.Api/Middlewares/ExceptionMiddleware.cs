using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Services;

namespace ProductCatalog.Api.Middlewares;

public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            BusinessException => HttpStatusCode.BadRequest,
            ArgumentException => HttpStatusCode.BadRequest,
            DbUpdateException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        if (statusCode == HttpStatusCode.InternalServerError)
            logger.LogError(exception, "Error inesperado en la API.");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            statusCode = context.Response.StatusCode,
            message = statusCode == HttpStatusCode.InternalServerError
                ? "Ocurrió un error inesperado. Intente nuevamente."
                : exception.Message
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}
