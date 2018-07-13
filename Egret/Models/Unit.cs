using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Egret.DataAccess;

namespace Egret.Models
{
    public partial class Unit
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        [Display(Name = "Sort Order")]
        public int SortOrder { get; set; }

        public bool Active { get; set; }
    }
}
