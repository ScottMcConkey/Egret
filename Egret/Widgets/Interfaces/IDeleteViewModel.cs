using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Widgets
{
    public interface IDeleteViewModel
    {
        string AccessGroupName { get; }

        string Id { get; }

        string ObjectName { get; }
    }
}
