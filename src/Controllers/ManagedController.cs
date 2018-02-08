using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Egret.DataAccess;

namespace Egret.Controllers
{
    [Authorize]
    public class ManagedController : Controller
    {
        protected EgretContext _context;

        public ManagedController(EgretContext context)
        {
            _context = context;
        }
    }
}