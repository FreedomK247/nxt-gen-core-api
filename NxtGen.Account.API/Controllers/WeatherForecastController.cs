using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NxtGen.Account.API.BusinessLogic.Contracts;
using NxtGen.Account.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEmailService _emailService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var message = new EmailMessageViewModel(new string[] { "freedom.khanyile@adaptit.com" }, "Test Email", "This is the content from email", null);
            await _emailService.SendEmailAsync(message);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// Send an email with Attachment
        /// </summary>
        /// <returns>Test Weather.</returns>
        [HttpPost]
        public async Task<IEnumerable<WeatherForecast>> Post()
        {
            var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();
            var message = new EmailMessageViewModel(new string[] { "freedom.khanyile@adaptit.com" }, "Test Email with attachment", "This is the content from our email with attachment", files);
            
            await _emailService.SendEmailAsync(message);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
