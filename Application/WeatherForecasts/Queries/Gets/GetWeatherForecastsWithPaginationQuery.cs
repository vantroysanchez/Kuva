using Application.Common.Models;
using Application.Common.Mappings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WeatherForecasts.Queries.Gets
{
    public record GetWeatherForecastsWithPaginationQuery: IRequest<PaginatedList<WeatherForecast>>
    {
        public int ListId { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetWeatherForecastsWithPaginationQueryHandler: IRequestHandler<GetWeatherForecastsWithPaginationQuery, PaginatedList<WeatherForecast>>
    {
        public GetWeatherForecastsWithPaginationQueryHandler()
        {

        }

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<PaginatedList<WeatherForecast>> Handle(GetWeatherForecastsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var rng = new Random();

            return Enumerable.Range(1, 20).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .Where(x => x.ListId == request.ListId)
            .PaginatedEnumerable(request.PageNumber, request.PageSize);
        }
    }
}
