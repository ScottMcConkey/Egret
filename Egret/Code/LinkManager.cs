using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Code
{
    public static class LinkManager
    {
        static string GetBackText(object method)
        {
            string test = method.GetType().Name + "Test" + method.GetType().ToString();

            return test;
        }
    }


}
