using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoursesApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new
            {
                Message = context.Exception.Message,
                StatusCode = 500,
                Time = DateTime.Now
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }

}
