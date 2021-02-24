
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
                if (toy.IsConnected() && toy.CheckHand(LHand))
                {
                    toy.vibrate(PluginConfig.Instance.Duration, PluginConfig.Instance.Intense);
                }
            }
        }

        public void vibrateActivePreset()
        {
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected())
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
    }
}
