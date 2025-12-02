using System.Diagnostics;

namespace CoursesApi.Middlewares
{
    public class RequestTimingMiddleware // دي يعتبر بتراقب الrequest وتشوف السرعه ال خدها قد اى وهكذا
    {
        private readonly RequestDelegate _next;

        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next=next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch=Stopwatch.StartNew();
            Console.WriteLine($"[Middleware] Request started {context.Request.Method} {context.Request.Path}");

            await _next(context);
            stopwatch.Stop();

            Console.WriteLine($"[Middleware] Request ended in {stopwatch.ElapsedMilliseconds} ms");

        }
    }
}
