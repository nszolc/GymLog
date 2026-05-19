using GymLog.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GymLog.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Not found: {Message}", ex.Message);
            await WriteProblemAsync(context, 404, "Not Found", ex.Message);
        }
        catch (ConflictException ex)
        {
            _logger.LogWarning(ex, "Conflict: {Message}", ex.Message);
            await WriteProblemAsync(context, 409, "Conflict", ex.Message);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation failed: {Message}", ex.Message);
            await WriteValidationProblemAsync(context, ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await WriteProblemAsync(context, 500, "Internal Server Error", "An unexpected error occurred.");
        }
    }
    
    private static async Task WriteProblemAsync(HttpContext context, int status, string title, string detail)
    {
        context.Response.StatusCode = status;
        context.Response.ContentType = "application/problem+json";
        
        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail
        };
        
        await context.Response.WriteAsJsonAsync(problem);
    }
    
    private static async Task WriteValidationProblemAsync(
        HttpContext context, 
        IReadOnlyDictionary<string, string[]> errors)
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "application/problem+json";
        
        var problem = new ValidationProblemDetails(new Dictionary<string, string[]>(errors))
        {
            Status = 400,
            Title = "Validation failed"
        };
        
        await context.Response.WriteAsJsonAsync(problem);
    }
}