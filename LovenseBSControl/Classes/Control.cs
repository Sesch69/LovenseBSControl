
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LovenseBSControl.Configuration;
using LovenseBSControl.Classes.Modus;
using System.Reflection;
using System.Linq;
using System.Collections;

using System.IO;
using UnityEngine;

namespace LovenseBSControl.Classes
{
    class Control
    {
        private List<Toy> Toys = new List<Toy>();

        private DefaultModus ActiveModus = new DefaultModus();

        public List<object> AvailableModi = new object[] { "Default" }.ToList();

        private Dictionary<string, DefaultModus> ModiList = new Dictionary<string, DefaultModus> {
            { "Default", new DefaultModus() }
        };

        public int HitCounter = 0;
        public int MissCounter = 0;

        private Classes.Request Request;
        public Control()
        {
            this.Request = new Classes.Request();
            this.loadModi();
            this.setModus();
        }

        public void setModus()
        {
            if (ModiList.ContainsKey(PluginConfig.Instance.modus))
            {
                this.ActiveModus = ModiList[PluginConfig.Instance.modus];
            }
            else
            {
                this.ActiveModus = new DefaultModus();
            }
        }

        public void Init()
        {
        }

        public async Task LoadToysAsync()
        {
            Toys = await this.Request.requestToysListAsync();
        }

        public List<Toy> getToyList()
        {
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

        public bool isToyAvailable()
        {
            return this.Toys.Count > 0;
        }

        public void pauseGame()
        {
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

        public void loadModi()
        {
            foreach (string obj in Utilities.GetAllClasses("LovenseBSControl.Classes.Modus"))
            {
                if (obj.Equals("Modus") || obj.Equals("DefaultModus")) continue;
                Type modi = Type.GetType("LovenseBSControl.Classes.Modus." + obj);
                if (modi != null)
                {
                    DefaultModus activeObj = Activator.CreateInstance(modi) as DefaultModus;
                    AvailableModi.Add(activeObj.GetModusName());
                    ModiList.Add(activeObj.GetModusName(), activeObj);
                }
            }
        }
    }
}
