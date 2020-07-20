
namespace Egret.Widgets.ViewModels
{
    public class ItemDeleteViewModel : IDeleteViewModel
    {
        public string AccessGroupName { get; } = "Item_Delete";

        public string Id { get; }

        public string ObjectName { get; } = "Item";

        public ItemDeleteViewModel(string id)
        {
            Id = id;
        }
        
    }
}
