using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NxtGen.Account.API.BusinessLogic.Contracts;
using NxtGen.Account.API.BusinessLogic.Helpers;
using NxtGen.Account.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;

        public EmailController(ILogger<EmailController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody]EmailMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = model;
                await _emailService.SendEmailAsync(message);
            }
            else
            {
                _logger.LogError("Oops! model is not valid");
            }
 
            return Ok(new
            {
                Message = $"Email sent successfully to {ProcessEmailAddresses(model.To)}",
                Date = DateTime.Now
            });
        }

        private string ProcessEmailAddresses(List<string> emails)
        {
            string emailList = "";
            foreach (var item in emails)
            {
                emailList += item + " ";
            }

            return emailList;
        }
    }
}
