using Application.Headers.Commands.Create;
using Application.Headers.Commands.Delete;
using Application.Headers.Commands.Update;
using Application.Headers.Queries.Gets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class HeaderController : ApiControllerBase
    {
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<HeaderVm>> Get()
        {
            return await Mediator.Send(new GetHeaderQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateHeaderCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateHeaderCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteHeaderCommand(id));

            return NoContent();
        }
    }
}
