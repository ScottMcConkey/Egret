using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret
{
    internal class SecurityModel
    {
        private Dictionary<string, string> _securityMatrix;

        public SecurityModel()
        {
            _securityMatrix.Add("Item_Edit", "Edit Item");
            _securityMatrix.Add("Item_Create", "Create Item");
            _securityMatrix.Add("Item_Delete", "Delete Item");
            _securityMatrix.Add("ConsumptionEvent_Edit", "Edit Consumption Event");
            _securityMatrix.Add("ConsumptionEvent_Create", "Create Consumption Event");
            _securityMatrix.Add("ConsumptionEvent_Delete", "Delete Consumption Event");
        }

        public string this[string index]
        {
            get
            {
                return _securityMatrix[index];
            }

        }
    }
}
