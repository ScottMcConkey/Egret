using System.ComponentModel.DataAnnotations;

namespace Egret.Models
{
    public class StorageLocation
    {
        public int StorageLocationId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }

        public bool Active { get; set; }
    }
}
