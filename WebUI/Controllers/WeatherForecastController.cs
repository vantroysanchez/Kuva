using Application.Common.Models;
using Application.WeatherForecasts.Queries.Gets;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class WeatherForecastController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }

        [HttpGet("GetWeatherWithPagination")]
        public async Task<ActionResult<PaginatedList<WeatherForecast>>> GetWeatherForecastWithPagination([FromQuery] GetWeatherForecastsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}