using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WeatherForecasts.Queries.Gets
{
    public record GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecast>>;

    public class GetWeatherForecastsQueryHandler : RequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecast>>
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        protected override IEnumerable<WeatherForecast> Handle(GetWeatherForecastsQuery request)
        {
            var rng = new Random();

            return Enumerable.Range(1, 20).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
    }
}
