using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovenseBSControl.Configuration
{
    public class ToysConfig
    {
        
        public String Id;

        public bool LHand;

        public bool RHand;

        public bool Inactive;
        
        public ToysConfig(String Id) {
            this.Id = Id;
            this.LHand = true;
            this.RHand = true;
            this.Inactive = false;
        }
    }
}
