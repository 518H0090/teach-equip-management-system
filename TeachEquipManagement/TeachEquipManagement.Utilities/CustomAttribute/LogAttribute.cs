using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace TeachEquipManagement.Utilities.CustomAttribute
{
    public class LogFilterAttribute : ActionFilterAttribute,
        IResourceFilter,
        IActionFilter,
        IResultFilter,
        IExceptionFilter
    {
        private readonly ILogger _logger;

        public LogFilterAttribute(ILogger logger)
        {
            _logger = logger;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _logger.Information($"Resource '{context.ActionDescriptor.DisplayName}' has completed.");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _logger.Information($"Resource '{context.ActionDescriptor.DisplayName}' is starting.");
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.Information($"Action '{context.ActionDescriptor.DisplayName}' is starting.");

            var result = await next();

            _logger.Information($"Action '{context.ActionDescriptor.DisplayName}' has completed");
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            _logger.Information($"Result '{context.ActionDescriptor.DisplayName}' is starting.");

            var result = await next();

            _logger.Information($"Result '{context.ActionDescriptor.DisplayName}' has completed");
        }


        public void OnException(ExceptionContext context)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            _logger.Error($"Exception: '{context.Exception.InnerException}' is thrown.");
        }
    }

}
