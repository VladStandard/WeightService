// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using WebApiCore.Controllers;
using WebApiScales.Models;

namespace WebApiScales.Controllers;

/// <summary>
/// WeatherForecast controller.
/// </summary>
//[Route("[controller]")]
public class WeatherForecastController : BaseController
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public WeatherForecastController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
	    //
    }
    
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v3/WeatherForecast/")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
        //.ToArray();
    }
}
