using Egret.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using Egret.DataAccess;
using Egret.Models;

namespace Egret.Code
{
    public class UserUtilities
    {
        private static ILogger _logger;
        private EgretContext _context;

        public UserUtilities(EgretContext context, ILogger<UserUtilities> logger)
        {
            _logger = logger;
            _context = context;
        }


    }
}
