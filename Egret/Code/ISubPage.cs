using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Code
{
    public interface ISubPage
    {
        string TabName { get; set; }
        string PageTitle { get; set; }
        string PreviousAction { get; set; }
        string PreviousController { get; set; }
    }
}
