using Egret.Interfaces;

namespace Egret.ViewModels
{
    public class ConsumptionEventDeleteViewModel : IDeleteModel
    {
        public string AccessGroupName { get; } = "ConsumptionEvent_Delete";

        public string Id { get; }

        public ConsumptionEventDeleteViewModel(string id)
        {
            Id = id;
        }
    }
}
