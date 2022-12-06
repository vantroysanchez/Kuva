using Application.Common.Models;
using Application.Details.Commands.Create;
using Application.Details.Commands.Delete;
using Application.Details.Commands.Update;
using Application.Details.Queries.GetDetailByHeader;
using Application.Details.Queries.GetDetailWithPagination;
using Application.Headers.Commands.Create;
using Application.Headers.Commands.Delete;
using Application.Headers.Commands.Update;
using Application.WeatherForecasts.Queries.Gets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class DetailController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<DetailDtoWithPagination>>> GetDetailWithPagination([FromQuery] GetDetailWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{headerId}")]
        public async Task<ActionResult<DetailByHeaderVm>> GetDetailByHeader(int headerId)
        {
            var detailByHeader = await Mediator.Send(new GetDetailByHeaderQuery(headerId));

            if (!detailByHeader.Lists.Any()) return NotFound();

            return detailByHeader;
            
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateDetailCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateDetailCommand command)
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
            await Mediator.Send(new DeleteDetailCommand(id));

            return NoContent();
        }

    }
}
