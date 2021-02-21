using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovenseBSControl.Classes
{
    public class Toy
    {
        private String ID;
        private String Type;
        private bool Connected;
        private String Motor;
        private bool LeftHand;
        private bool RightHand;
        private String NickName;

        public Toy(String ID, String Type, bool Connected = false, String NickName = "", String Motor = "", bool LeftHand = false, bool RightHand = false) {
            this.ID = ID;
            this.Type = Type;
            this.Motor = Motor;

            this.LeftHand = LeftHand;
            this.RightHand = RightHand;

            this.Connected = Connected;
            this.NickName = NickName;
        }

        public bool isConnected() {
            return this.Connected;
        }

        public String getId() {
            return ID;
        }

        public String getMotor() {
            return this.Motor;
        }

        public void setHands(bool LeftHand, bool RightHand) {
            this.LeftHand = LeftHand;
            this.RightHand = RightHand;
        }

    }
}
