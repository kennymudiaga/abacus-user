using AbacusUser.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AbacusUser.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync(SignUpCommand command)
        {
            var result = await mediator.Send(command);
            return result.ToActionResult();
        }
    }
}
