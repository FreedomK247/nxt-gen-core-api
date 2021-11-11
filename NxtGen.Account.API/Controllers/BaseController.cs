using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities = NxtGen.Account.API.Data.Entities;

namespace NxtGen.Account.API.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        // returns the current authenticated account (null if not logged in)
        public Entities.Account Account => (Entities.Account)HttpContext.Items["Account"];
    }
}
