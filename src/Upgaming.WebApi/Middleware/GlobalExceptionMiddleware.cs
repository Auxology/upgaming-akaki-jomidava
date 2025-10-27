using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Upgaming.WebApi.Middleware;

/// <summary>
/// Provides custom middleware for exceptions.
/// This is meant to follow structure of CustomResults.cs.
/// </summary>
internal sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        _logger.LogError(exception, exception.Message);
        
        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        
        Activity? activity = httpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        
        string title = exception switch
        {
            ValidationException => "One or more validation errors occurred.",
            _ => "An unexpected error occurred."
        };

        string type = exception switch
        {
            ValidationException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };

        var problemDetails = new
        {
            Type = type,
            Title = title,
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method}:{httpContext.Request.Path}",
            Status = httpContext.Response.StatusCode,
            Extensions = new Dictionary<string, object?>
            {
                ["requestId"] = httpContext.TraceIdentifier,
                ["traceId"] = activity?.Id
            }
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        await JsonSerializer.SerializeAsync(httpContext.Response.Body, problemDetails, jsonOptions);
    }
}