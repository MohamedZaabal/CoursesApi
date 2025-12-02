using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoursesApi.Filters
{
    public class ActionLoggingFilter : IActionFilter //يعتبر ذي ال ميدل وير بس ده ممكن اخصصه ل جزء واحد بس عادي 
    {
        private Stopwatch? _stopwatch;
       

        public void OnActionExecuting(ActionExecutingContext context)
        {
           _stopwatch= Stopwatch.StartNew();
            Console.WriteLine($"[ActionFilter] Starting action:{context.ActionDescriptor.DisplayName}");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
           _stopwatch?.Stop();
            Console.WriteLine($"[ActionFilter] Finished action :{context.ActionDescriptor.DisplayName} in {_stopwatch?.ElapsedMilliseconds} ms");
        }
    }
}
