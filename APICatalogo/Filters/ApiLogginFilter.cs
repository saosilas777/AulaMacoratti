using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
	public class ApiLogginFilter : IActionFilter
	{
		private readonly ILogger<ApiLogginFilter> _logger;

		public ApiLogginFilter(ILogger<ApiLogginFilter> logger)
		{
			_logger = logger;
		}
		
		public void OnActionExecuting(ActionExecutingContext context)
		{
			_logger.LogInformation("Executando em OnActionExecuting");
			_logger.LogInformation("_______________________________");
			_logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
			_logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
			_logger.LogInformation("_______________________________");
		}
		public void OnActionExecuted(ActionExecutedContext context)
		{
			_logger.LogInformation("Executando em OnActionExecuted");
			_logger.LogInformation("_______________________________");
			_logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
			_logger.LogInformation($"ModelState: {context.HttpContext.Response.StatusCode}");
			_logger.LogInformation("_______________________________");
		}
	}
}
