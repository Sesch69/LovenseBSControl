using LovenseBSControl.Configuration;
using System;
using System.Collections.Generic;

namespace LovenseBSControl.Classes.Modus
{
    class Challenge1Modus : Modus
    {

        public override void HandleHit(List<Toy> toys, bool LHand, bool success)
        {
            if (success)
            {
                if (Plugin.Control.HitCounter >= 15)
                {
                    Plugin.Control.MissCounter = Math.Max(--Plugin.Control.MissCounter, 0);
                    Plugin.Control.HitCounter = 0;
                }
            }
            else 
            {
                Plugin.Control.HitCounter = 0;
            }
            
            this.HandleMiss(toys, LHand);
            
        }

        public override void HandleMiss(List<Toy> toys, bool LHand)
        {
            Plugin.Control.MissCounter = Math.Min(Plugin.Control.MissCounter,20);

            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive())
                {
                    toy.vibrate(0, Plugin.Control.MissCounter);
                }
            }

        }

        public override void HandleBomb(List<Toy> toys)
        {
            if (!PluginConfig.Instance.VibeBombs) return;

            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive())
                {
                    toy.vibratePreset(3 , true);
                }
            }
        }

        public override string GetModusName()
        {
            return "Challenge 1";
        }
    }
}
