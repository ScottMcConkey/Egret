
namespace Egret.Widgets.ViewModels
{
    public class ConsumptionEventDeleteViewModel : IDeleteViewModel
    {
        public string AccessGroupName { get; } = "ConsumptionEvent_Delete";

        public string Id { get; }

        public string ObjectName { get; } = "Consumption Event";

        public ConsumptionEventDeleteViewModel(string id)
        {
            Id = id;
        }

    }
}
