using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.ViewModels
{
    public class ObjectInformationViewModel
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ObjectInformationViewModel(string createdBy, DateTime? createdDate, string updatedBy, DateTime? updatedDate)
        {
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            UpdatedBy = updatedBy;
            UpdatedDate = updatedDate;
        }
    }
}
