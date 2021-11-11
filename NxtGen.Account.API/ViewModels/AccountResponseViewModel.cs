using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.ViewModels
{
    public class AccountResponseViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public bool IsVerified { get; set; }
    }
}
