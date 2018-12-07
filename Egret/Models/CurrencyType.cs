using System.ComponentModel.DataAnnotations;

namespace Egret.Models
{
    public partial class CurrencyType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        public string Symbol { get; set; }

        [Display(Name = "Sort Order")]
        [Required]
        public int SortOrder { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Default")]
        public bool DefaultSelection { get; set; }
    }
}
