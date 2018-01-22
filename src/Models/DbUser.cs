using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models
{
    public class DbUser
    {
        public string Name { get; set; }
        public int Usesysid { get; set; }
        public bool Usecreatedb { get; set; }
        public bool Usesuper { get; set; }
        public bool Userepl { get; set; }
        public bool Usebypassrls { get; set; }
        public string Passwd { get; set; }
        public DateTime Valuntil { get; set; }
        public string[] Useconfig { get; set; }

    }
}
