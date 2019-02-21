using Egret.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using Egret.DataAccess;

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

        public void RebuildUserRoles(IdentityDbContext context, User user)
        {
            User _user = _context.Users.Where(x => x.Id == user.Id).SingleOrDefault();
        }
    }
}
