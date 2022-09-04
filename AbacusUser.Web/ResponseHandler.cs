using AbacusUser.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbacusUser.Web;

public static class ResponseHandler
{
    public static IActionResult HandleModelStateError(ActionContext arg)
    {
        string errorMessage = string.Join(Environment.NewLine, arg.ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
        return new BadRequestObjectResult(errorMessage);
    }

    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if(result.Success) return new OkObjectResult(result);
        return new BadRequestObjectResult(result);
    }
}
