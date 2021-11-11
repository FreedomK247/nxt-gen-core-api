using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.ViewModels
{
    public class ValidateResetTokenRequestViewModel
    {
        [Required]
        public string Token { get; set; }
    }
}
