using Egret.DataAccess;
using Microsoft.Extensions.Logging;

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
