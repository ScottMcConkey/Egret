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
    /// The View Model is used for displaying related Inventory Item objects.
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

        public string Title { get; set; } = "Inventory Item";
        public bool HasCreateNew { get; set; } = false;
        public string SourceId { get; set; }
        public string Area { get; set; } = "Inventory";
        public string Controller { get; set; } = "Items";
        public string CreateAction { get; set; } = "Create";
        public string EditRole { get; set; } = "Item_Edit";
        public string ReadOnlyRole { get; set; } = "Item_Read";
        public string HelpText { get; set; } = "";
        public string ObjectDisplay { get; set; } = "RelationshipDisplay";
        public string TargetObjectIdColumn { get; set; } = "Code";
        public string CssClass { get; set; } = "egret egret-add";
        public ICollection RelationshipObjects { get; set; }
    }
}
