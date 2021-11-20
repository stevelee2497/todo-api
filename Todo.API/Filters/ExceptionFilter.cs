using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Todo.API.Filters
{
    public class ExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is Exception exception)
            {
                var response = new ErrorResponse
                {
                    Message = exception.Message,
                    StackTrace = exception.StackTrace
                };
                context.Result = new ObjectResult(response)
                {
                    StatusCode = exception is IApplicationException ex ? ex.StatusCode : (int)HttpStatusCode.InternalServerError,
                };
                context.ExceptionHandled = true;
            }
        }
    }

    public interface IApplicationException
    {
        int StatusCode { get; }
    }

    public class InternalErrorException : Exception, IApplicationException
    {
        public int StatusCode => (int)HttpStatusCode.InternalServerError;

        public InternalErrorException(string message) : base(message) { }
    }

    public class ErrorResponse
    {
        public string? Message { get; set; }

        public string? StackTrace { get; set; }
    }
}
