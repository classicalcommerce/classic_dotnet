using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace API.Filters;

public class LoggingActionFilter(ILogger<LoggingActionFilter> logger) : IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger = logger;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        string controllerName = actionDescriptor?.ControllerName ?? "UnknownController";
        string actionName = actionDescriptor?.ActionName ?? "UnknownAction";

        _logger.LogInformation("Executing Controller: {Controller} - Method: {Action}", controllerName, actionName);

        foreach (var param in context.ActionArguments)
        {
            _logger.LogInformation("Parameter: {ParamName} = {ParamValue}", param.Key, param.Value);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        string controllerName = actionDescriptor?.ControllerName ?? "UnknownController";
        string actionName = actionDescriptor?.ActionName ?? "UnknownAction";

        if (context.Exception == null)
        {
            _logger.LogInformation("Successfully executed Controller: {Controller} - Method: {Action}", controllerName, actionName);
        }
        else
        {
            _logger.LogError(context.Exception, "Exception in Controller: {Controller} - Method: {Action}", controllerName, actionName);
        }
    }
}
