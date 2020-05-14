using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models
{
    public class StorageLocation
    {
        public int StorageLocationId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
