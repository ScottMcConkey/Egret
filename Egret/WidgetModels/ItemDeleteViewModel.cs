using Egret.Interfaces;

namespace Egret.ViewModels
{
    public class ItemDeleteViewModel : IDeleteModel
    {
        public string AccessGroupName { get; } = "Item_Delete";

        public string Id { get; }

        public ItemDeleteViewModel(string id)
        {
            Id = id;
        }
    }
}
