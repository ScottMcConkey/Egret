using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egret.Models;
using Egret.Interfaces;

namespace Egret.ViewModels
{
    public class SearchResults<T>
    {
        public ISearchParameters SearchParameters { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public List<T> Results { get; set; }
    }
}
