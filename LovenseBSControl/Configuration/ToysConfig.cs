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

        public bool Random;

        public string HType;
        
        public ToysConfig(String Id) {
            this.Id = Id;
            this.LHand = true;
            this.RHand = true;
            this.Inactive = false;
            this.Random = false;
            this.HType = HTypes.bHands;
        }

        public void setHType(String value) {
            this.HType = value;
            this.Inactive = false;
            this.Random = false;
            if (value.Equals(HTypes.bHands))
            {
                this.LHand = true;
                this.RHand = true;
            }
            else if (value.Equals(HTypes.lHand))
            {
                this.LHand = true;
                this.RHand = false;
            }
            else if (value.Equals(HTypes.rHand))
            {
                this.LHand = false;
                this.RHand = true;
            }
            else if (value.Equals(HTypes.random))
            {
                this.Random = true;
            }
            else if (value.Equals(HTypes.inactive))
            {
                this.Inactive = true;
            }

        }
    }

    public static class HTypes
    {
        public const string bHands = "Both Hands";
        public const string lHand = "Left Hand";
        public const string rHand = "Right Hand";
        public const string random = "Random";
        public const string inactive = "Inactive";
    }

    
}
