using System.ComponentModel.DataAnnotations;

namespace Egret.Models
{
    public partial class CurrencyType
    {
        public int CurrencyTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        public string Symbol { get; set; }

    }
}
