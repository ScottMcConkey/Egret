using Egret.Models;
using System.Collections;

namespace Egret.Widgets.ViewModels
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
        public string EditRole { get; set; } = "ConsumptionEvent_Edit";
        public string ReadOnlyRole { get; set; } = "ConsumptionEvent_Read";
        public string HelpText { get; set; } = "Create New Consumption Event";
        public string ObjectDisplay { get; set; } = nameof(ConsumptionEvent.RelationshipDisplay);
        public string TargetObjectIdColumn { get; set; } = nameof(ConsumptionEvent.ConsumptionEventId);
        public string CssClass { get; set; } = "egret egret-add";
        public ICollection RelationshipObjects { get; set; }
    }
}
