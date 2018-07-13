using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egret.Interfaces;
using Egret.Models;

namespace Egret.ViewModels
{
    /// <summary>
    /// The View Model used for displaying related Inventory Item objects.
    /// </summary>
    public class RelationshipViewModelInventory : IRelationshipViewModel
    {
        public RelationshipViewModelInventory(ICollection relObjects, string sourceId)
        {
            SourceId = sourceId;
            RelationshipObjects = relObjects;
        }

        public RelationshipViewModelInventory(ICollection relObjects)
        {
            RelationshipObjects = relObjects;
        }

        public string Title { get; set; } = "Inventory Items";
        public bool HasCreateNew { get; set; } = false;
        public string SourceId { get; set; }
        public string Area { get; set; } = "";
        public string Controller { get; set; } = "Inventory";
        public string Action { get; set; } = "Edit";
        public string HelpText { get; set; } = "";
        public string ObjectDisplay { get; set; } = "RelationshipDisplay";
        public string TargetObjectIdColumn { get; set; } = "Code";
        public string CssClass { get; set; } = "glyphicon glyphicon-plus";
        public ICollection RelationshipObjects { get; set; }
    }
}
