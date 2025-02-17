namespace MedTime.Api.Middleware;

using System.Text.Json;
using MedTime.Domain.Exceptions;

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
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred during request processing");
			await HandleExceptionAsync(context, ex);
		}
	}

	private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";

		context.Response.StatusCode = exception switch
		{
			DomainException => StatusCodes.Status400BadRequest,
			KeyNotFoundException => StatusCodes.Status404NotFound,
			_ => StatusCodes.Status500InternalServerError
		};

		var response = new
		{
			status = context.Response.StatusCode,
			message = exception.Message,
			detail = exception is DomainException ? exception.Message : "An error occurred while processing your request."
		};

		await context.Response.WriteAsync(JsonSerializer.Serialize(response));
	}
}

public static class ExceptionHandlingMiddlewareExtensions
{
	public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<ExceptionHandlingMiddleware>();
	}
}