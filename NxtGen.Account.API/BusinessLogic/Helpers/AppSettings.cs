using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.BusinessLogic.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }

        // refresh token time to live (in x time), inactive tokens are
        // automatically deleted from the database after this time
        public int RefreshTokenTTL { get; set; }
       
    }
}
