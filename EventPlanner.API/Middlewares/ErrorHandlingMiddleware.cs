using EventPlanner.Domain.Exceptions;

namespace EventPlanner.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (BadRequestException badRequest)
        {
            logger.LogWarning(badRequest.Message);

            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(badRequest.Message);
        }
        catch (NotFoundException notFound)
        {
            logger.LogWarning(notFound.Message);

            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFound.Message);
        }
        catch (ForbidException forbid)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access forbidden");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}
