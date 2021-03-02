
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LovenseBSControl.Configuration;
using LovenseBSControl.Classes.Modus;


namespace LovenseBSControl.Classes
{
    class Control
    {
        private List<Toy> Toys = new List<Toy>();

        private DefaultModus ActiveModus = new DefaultModus();

        public int HitCounter = 0;
        public int MissCounter = 0;

        private Classes.Request Request;
        public Control() {
            this.Request = new Classes.Request();
            this.setModus();
        }

        public void setModus() {
            if (PluginConfig.Instance.modus.Equals("Default")){
                this.ActiveModus = new DefaultModus();
            }else if (PluginConfig.Instance.modus.Equals("Challenge 1")){
                this.ActiveModus = new Challenge1Modus();
            }
        }

        public void Init() { 
        }

        public async Task LoadToysAsync() {
            Toys = await this.Request.requestToysListAsync();
        }

        public List<Toy> getToyList() {
            return Toys;
        }

        public void handleCut(bool LHand, bool success)
        {
            if (success)
            {
                Plugin.Control.HitCounter++;
            }
            else
            {
                Plugin.Control.MissCounter++;
            }
            this.ActiveModus.HandleHit(Toys, LHand, success);
        }

        public void handleBomb()
        {
            this.ActiveModus.HandleBomb(this.Toys);
        }

        public void stopActive()
        {
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected())
                {
                    toy.stop(true);
                }
            }
        }

        public bool isToyAvailable() {
            return this.Toys.Count > 0;
        }

        public void pauseGame() {
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected())
                {
                    toy.stop();
                }
            }
        }

        public void resumeGame()
        {
            foreach (Toy toy in this.Toys)
            {
                if (toy.IsConnected())
                {
                    toy.resume();
                }
            }
        }

        public void endGame()
        {
            this.stopActive();
        }
    }
}
