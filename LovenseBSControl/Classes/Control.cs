
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LovenseBSControl.Configuration;


namespace LovenseBSControl.Classes
{
    class Control
    {
        private List<Toy> Toys = new List<Toy>();

        public int HitCounter = 0;
        public int MissCounter = 0;

        private Classes.Request Request;
        public Control() {
            this.Request = new Classes.Request();
        }

        public void Init() { 
        }

        public async Task LoadToysAsync() {
            Toys = await this.Request.requestToysListAsync();
        }

        public List<Toy> getToyList() {
            return Toys;
        }

        public void vibrateActive(bool LHand) {
            
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected() && toy.IsActive()) 
                {
                    if (toy.CheckHand(LHand) && !toy.getToyConfig().Random)
                    {
                        toy.vibrate(PluginConfig.Instance.Duration);
                    }
                    if (toy.getToyConfig().Random) {
                        Random rng = new Random();
                        bool random = rng.Next(0, 2) > 0;
                        if ((random && LHand) || (!random && !LHand)) {
                            toy.vibrate(PluginConfig.Instance.Duration);
                        }
                    }
                }
            }
        }

        public void vibrateActivePreset()
        {
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected() && toy.IsActive())
                {
                    toy.vibratePreset();
                }
            }

        }

        public void stopActive()
        {
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected())
                {
                    toy.stop();
                }
            }
        }

        public bool isToyAvailable() {
            return this.Toys.Count > 0;
        }
    }
}
