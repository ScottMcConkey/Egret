﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Interfaces
{
    public interface IRelationshipViewModel
    {
        string Title { get; set; }
        bool HasCreateNew { get; set; }
        string SourceId { get; set; }
        string Area { get; set; }
        string Controller { get; set; }
        string CreateAction { get; set; }
        string EditRole { get; set; }
        string ReadOnlyRole { get; set; }
        string HelpText { get; set; }
        string ObjectDisplay { get; set; }
        string TargetObjectIdColumn { get; set; }
        string CssClass { get; set; }

        ICollection RelationshipObjects { get; set; }
    }
}
