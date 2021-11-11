using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.ViewModels
{
    public class ForgotPasswordRequestViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
