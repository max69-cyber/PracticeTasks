namespace PracticeTasks.Middleware;

public class ParallelLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly SemaphoreSlim _semaphore;

    public ParallelLimitMiddleware(RequestDelegate next, int parallelLimit)
    {
        _next = next;
        _semaphore = new SemaphoreSlim(parallelLimit);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!await _semaphore.WaitAsync(0))
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            return;
        }

        try
        {
            await _next(context);
        }
        finally
        {
            _semaphore.Release();
        }
    }
}