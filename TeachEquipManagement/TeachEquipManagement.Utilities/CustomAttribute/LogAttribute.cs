using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace TeachEquipManagement.Utilities.CustomAttribute
{
    public class LogFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly ILogger _logger;

        public LogFilterAttribute(ILogger logger)
        {
            _logger = logger;
        }

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    _logger.Information($"Action '{context.ActionDescriptor.DisplayName}' is starting.");

        //    base.OnActionExecuting(context);
        //}

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    _logger.Information($"Action '{context.ActionDescriptor.DisplayName}' has completed");

        //    base.OnActionExecuted(context);
        //}

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.Information($"Action '{context.ActionDescriptor.DisplayName}' is starting.");
            var result = await next();
            _logger.Information($"Action '{context.ActionDescriptor.DisplayName}' has completed");
        }
    }

    class MyTestAttribute : Attribute
    {

    }
}
