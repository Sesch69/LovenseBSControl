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

        private int battery;

        private ToysConfig Config;

        private bool on;
        private int lastLevel;

        private String LastConnection;

        public Toy(String ID, String Type, bool Connected = false, String Version = "", String NickName = "", String Motor = "") {
            this.ID = ID;
            this.Type = Type;
            this.Motor = Motor;

            ToysConfig newConfig = ToysConfig.createToyConfig(this.GetId());
            if (PluginConfig.Instance.ToyConfigurations != null)
            {
                if (PluginConfig.Instance.ToyConfigurations != null && PluginConfig.Instance.IsAdded(this.ID))
                {
                    newConfig = PluginConfig.Instance.getToyConfig(this.ID);
                }
                else
                {
                    PluginConfig.Instance.AddToyConfiguration(this.ID,newConfig);
                }
            }
            Config = newConfig;
            this.Connected = Connected;
            this.NickName = NickName;
            this.Version = Version;
            this.on = false;
        }

        public string GetPictureName()
        {
            return ("logo_" + this.Type + this.Version + ".png").ToLower();
        }

        public bool IsConnected() {
            return this.Connected;
        }

        public bool IsActive() {
            return !this.Config.Inactive;
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

        public String GetNickName() {
            return this.NickName.Equals("") ? this.GetText() : this.NickName;
        }

        public String GetText() {
            return this.Version.Equals("") ? this.Type : this.Type + " " + this.Version;
        }

        public bool CanRotate() {

            return this.Type.Equals("Nora");
        }

        public bool CanPump()
        {
            return this.Type.Equals("Max");
        }

        public void Test() {
           this.vibrate(500, 10);
            
            //Request request = new Classes.Request();
            //request.TestToy(this).ConfigureAwait(true);
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
            this.lastLevel = level;
            request.UseToy(this, time, level).ConfigureAwait(true);
        }

        public void vibrate(int time, bool hit = true)
        {
            this.on = true;
            Request request = new Classes.Request();
            this.lastLevel = this.getIntense(hit);
            request.UseToy(this, time, this.lastLevel).ConfigureAwait(true);
        }

        public void resume() {
            Request request = new Classes.Request();
            request.UseToy(this, 0, this.lastLevel).ConfigureAwait(true);
        }

        private int getIntense(bool hit = true) {
            var intense = hit ? PluginConfig.Instance.IntenseHit : PluginConfig.Instance.IntenseMiss;
            if (PluginConfig.Instance.RandomIntenseHit) {
                Random rng = new Random();
                intense = rng.Next(1, 20);
            }
            return intense;
        }

        public void vibratePreset(int preset = 2, bool resume = false)
        {
            this.on = true;
            Request request = new Classes.Request();
            request.PresetToy(this, preset).ConfigureAwait(true);
        }

        public String getBattery()
        {
            return this.battery.ToString();
        }

        public void stop(bool resetLastLevel = false) {
            if (resetLastLevel)
            {
                this.lastLevel = 0;
            }
            this.on = false;
            Request request = new Classes.Request();
            request.StopToy(this).ConfigureAwait(true);
        }

        public ToysConfig GetToyConfig()
        {
            return Config;
        }

        public void SetBattery(int battery) {
            this.battery = battery;
        }

        public void SetConnection(string connectionName)
        {
            this.LastConnection = connectionName;
        }

        public string GetConnection()
        {
            return this.LastConnection;
        }

    }
}
