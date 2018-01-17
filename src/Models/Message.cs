using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models
{
    public class Message
    {
        public string Text { get; set; }

        public string Status { get; set; }

        public Message(string msg, string status)
        {
            Text = msg;
            Status = Status;
        }

        
    }
}
