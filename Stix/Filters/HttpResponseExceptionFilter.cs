using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Stix.Models;

namespace Stix.Filters;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    private readonly ILogger<HttpResponseExceptionFilter> _logger;

    public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger)
    {
        _logger = logger;
    }
    
    private readonly Dictionary<Type, int> _exceptionStatusCodes = new()
    {
        { typeof(KeyNotFoundException), StatusCodes.Status404NotFound },
        { typeof(DuplicateNameException), StatusCodes.Status400BadRequest }
    };

    public int Order => int.MaxValue - 10;
    
    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null) { return; }
        
        _logger.LogError(context.Exception.ToString());

        var errorDetails = new ErrorDetails(StatusCodes.Status500InternalServerError, "Something went wrong.");
        var type = context.Exception.GetType();
        if (_exceptionStatusCodes.TryGetValue(type, out var statusCode))
        {
            errorDetails.StatusCode = statusCode;
            errorDetails.Message = context.Exception.Message;
        }

        context.Result = new ObjectResult(errorDetails)
        {
            StatusCode = statusCode
        };

        context.ExceptionHandled = true;
    }
}