using AbacusUser.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace AbacusUser.Web;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        this.logger = logger;
    }
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case NotPermittedException:
                context.Result = new ForbidResult();
                break;
            case UnauthorizedException unEx:
                context.Result = new UnauthorizedObjectResult(unEx.Message);
                break;
            case NotFoundException notEx:
                context.Result = new NotFoundObjectResult(notEx.Message);
                break;
            case BusinessException bizEx:
                context.Result = new BadRequestObjectResult(bizEx.Message);
                if (bizEx.LogData != null)
                {
                    logger.LogError("Business error for request {path} - {data}", context.HttpContext.Request.Path, JsonSerializer.Serialize(bizEx.LogData));
                }
                break;
            case ApplicationException appEx:
                context.Result = new BadRequestObjectResult(appEx.Message);
                logger.LogError("Application Exception for request {path} - {message}:: StackTrace:: {stacktrace}",
                    context.HttpContext.Request.Path, appEx.Message, appEx.StackTrace);
                break;
            default:
                context.Result = new ObjectResult("Oops! Something went wrong.") { StatusCode = 500 };
                var message = context.Exception.GetBaseException()?.Message ?? "Oops! Something went wrong.";
                logger.LogError("Application Exception for request {path} - {message}:: StackTrace:: {stacktrace}",
                    context.HttpContext.Request.Path, message, context.Exception.StackTrace);
                break;
        }
        context.ExceptionHandled = true;
    }
}
