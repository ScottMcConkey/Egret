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
    /// The View Model used for displaying related Consumption Event objects.
    /// </summary>
    public class RelationshipViewModelConsumption : IRelationshipViewModel
    {
        public RelationshipViewModelConsumption(ICollection relObjects, string sourceId)
        {
            SourceId = sourceId;
            RelationshipObjects = relObjects;
        }

        public RelationshipViewModelConsumption(ICollection relObjects)
        {
            RelationshipObjects = relObjects;
        }

        public string Title { get; set; } = "Consumption Events";
        public bool HasCreateNew { get; set; } = true;
        public string SourceId { get; set; }
        public string Area { get; set; } = "Inventory";
        public string Controller { get; set; } = "ConsumptionEvents";
        public string CreateAction { get; set; } = "CreateFromItem";
        public string EditAction { get; set; } = "Edit";
        public string HelpText { get; set; } = "Create New Consumption Event";
        public string ObjectDisplay { get; set; } = "RelationshipDisplay";
        public string TargetObjectIdColumn { get; set; } = "Id";
        public string CssClass { get; set; } = "glyphicon glyphicon-plus";
        public ICollection RelationshipObjects { get; set; }
    }
}
