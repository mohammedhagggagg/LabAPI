using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

public class AppNameHeaderFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        context.HttpContext.Response.Headers["AppName"] = "API Day 03";
    }
}
