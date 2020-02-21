using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Egret.DataAccess;
using Egret.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Egret.Extensions;

namespace Egret.Utilities
{
    /// <summary>
    /// Factory object for Select Lists generated from database relationships.
    /// </summary>
    public class SelectListFactory
    {
        private readonly EgretContext _egretContext;

        public SelectListFactory(EgretContext context)
        {
            _egretContext = context;
            _egretContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        



        

        

        



        
    }
}
