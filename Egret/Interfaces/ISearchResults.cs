using Egret.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Interfaces
{
    interface ISearchResults
    {
        ISearchParameters SearchParameters { get; set; }

        PagingInfo PagingInfo { get; set; }
    }
}
