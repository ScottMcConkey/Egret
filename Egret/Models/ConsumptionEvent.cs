using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Egret.Attributes;
using Egret.Interfaces;

namespace Egret.Models
{
    public class ConsumptionEvent : IAuditable
    {
        [Language(Name = "Nepali", Value = "आईडी")]
        public string Id { get; set; }

        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; }

        [Display(Name = "Added By")]
        public string AddedBy { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? DateUpdated { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Required]
        [Display(Name = "Quantity Consumed")]
        [Language(Name = "Nepali", Value = "मात्रा खपत भयो")]
        [DisplayFormat(DataFormatString = "{0:0.####}", ApplyFormatInEditMode = true)]
        public decimal? QuantityConsumed { get; set; }

        [Required]
        [Display(Name = "Consumed By")]
        [Language(Name = "Nepali", Value = "द्वारा उपभोग गरियो")]
        public string ConsumedBy { get; set; }

        [Required]
        [Display(Name = "Date Consumed")]
        [UIHint("date")]
        [Language(Name = "Nepali", Value = "मिति खपत भयो")]
        public DateTime? DateOfConsumption { get; set; }

        [Display(Name = "Order Number")]
        [Language(Name = "Nepali", Value = "अर्डर नम्बर")]
        public string OrderNumber { get; set; }

        [Display(Name = "Pattern Number")]
        [Language(Name = "Nepali", Value = "ढाँचा नम्बर")]
        public string PatternNumber { get; set; }

        [Language(Name = "Nepali", Value = "टिप्पणीहरू")]
        public string Comments { get; set; }

        [Required]
        [Display(Name = "Item Code")]
        [Language(Name = "Nepali", Value = "वस्तु कोड")]
        public string InventoryItemCode { get; set; }

        public InventoryItem InventoryItemNavigation { get; set; }

        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string RelationshipDisplay
        {
            get
            {
                return (DateOfConsumption != null ? DateOfConsumption.Value.ToShortDateString() + " - " : "")
                    + "Quantity Consumed: " + (QuantityConsumed != null ? QuantityConsumed.ToString() : "0");
            }
            private set { }
        }
    }
}
