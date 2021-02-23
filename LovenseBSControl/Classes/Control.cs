
using System.Collections.Generic;
using System.Threading.Tasks;
using LovenseBSControl.Configuration;


namespace LovenseBSControl.Classes
{
    class Control
    {
        private List<Toy> Toys = new List<Toy>();

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

        public void vibrateActive() {
            
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected())
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
