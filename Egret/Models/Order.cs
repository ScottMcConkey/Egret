using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models
{
    public class Order
    {
        [Required]
        public string Id { get; set; }
    }
}
