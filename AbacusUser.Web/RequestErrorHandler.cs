using Microsoft.AspNetCore.Mvc;

namespace AbacusUser.Web;

public static class RequestErrorHandler
{
    public static IActionResult HandleModelStateError(ActionContext arg)
    {
        string errorMessage = string.Join(Environment.NewLine, arg.ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
        return new BadRequestObjectResult(errorMessage);
    }
}
