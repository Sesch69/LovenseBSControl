using System;

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
        private String Version;

        private bool on;

        public Toy(String ID, String Type, bool Connected = false, String Version = "", String NickName = "", String Motor = "", bool LeftHand = false, bool RightHand = false) {
            this.ID = ID;
            this.Type = Type;
            this.Motor = Motor;

            this.LeftHand = LeftHand;
            this.RightHand = RightHand;

            this.Connected = Connected;
            this.NickName = NickName;

            this.Version = Version;

            this.on = false;
        }

        public bool IsConnected() {
            return this.Connected;
        }

        public bool IsOn() {
            return this.on;
        }

        public String GetId() {
            return this.ID;
        }

        public String GetMotor() {
            return this.Motor;
        }

        public void SetHands(bool LeftHand, bool RightHand) {
            this.LeftHand = LeftHand;
            this.RightHand = RightHand;
        }

        public String getNickName() {
            return this.NickName.Equals("")?this.getText():this.NickName;
        }

        public String getText() {
            return this.Version.Equals("")?this.Type:this.Type + " " + this.Version;
        }

        public bool canRotate() {

            return this.Type.Equals("Nora");
        }

        public void test() {
            this.vibrate(500, 10);
        }

        public void setOff() {
            this.on = false;
        }
        internal void setOn()
        {
            this.on = true;
        }

        public void vibrate(int time, int level) {
            if (!this.on)
            {
                this.on = true;
                Request request = new Classes.Request();
                request.UseToy(this, time, level).ConfigureAwait(true);
            }
        }

        public void vibratePreset(int preset = 2)
        {
            if (!this.on)
            {
                this.on = true;
                Request request = new Classes.Request();
                request.PresetToy(this, preset).ConfigureAwait(true);
            }
        }



        public void stop() {
            if (this.on)
            {
                this.on = false;
                Request request = new Classes.Request();
                request.StopToy(this).ConfigureAwait(true);
            }
        }

    }
}
