using System;
using LovenseBSControl.Configuration;

namespace LovenseBSControl.Classes
{
    public class Toy
    {
        private String ID;
        private String Type;
        private bool Connected;
        private String Motor;
        private String NickName;
        private String Version;

        private ToysConfig Config;

        private bool on;

        public Toy(String ID, String Type, bool Connected = false, String Version = "", String NickName = "", String Motor = "") {
            this.ID = ID;
            this.Type = Type;
            this.Motor = Motor;

            Config = new ToysConfig(this.GetId());

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

        public bool switchLHand() {
            this.Config.LHand = !this.Config.LHand;
            return this.Config.LHand;
        }

        public bool switchRHand() {
            this.Config.RHand = !this.Config.RHand;
            return this.Config.RHand;
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

        public bool CheckHand(bool LHand) {
            return (LHand == this.Config.LHand) || (!LHand == this.Config.RHand);
        }

        public void vibrate(int time, int level) {
                this.on = true;
                Request request = new Classes.Request();
                request.UseToy(this, time, level).ConfigureAwait(true);
        }

        public void vibratePreset(int preset = 2)
        {
                this.on = true;
                Request request = new Classes.Request();
                request.PresetToy(this, preset).ConfigureAwait(true);
        }



        public void stop() {
                this.on = false;
                Request request = new Classes.Request();
                request.StopToy(this).ConfigureAwait(true);
        }


        public ToysConfig getToyConfig()
        {
            return Config;
        }

    }
}
