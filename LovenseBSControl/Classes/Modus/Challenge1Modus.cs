using LovenseBSControl.Configuration;
using System;
using System.Collections.Generic;

namespace LovenseBSControl.Classes.Modus
{
    class Challenge1Modus : Modus
    {

        public override void HandleHit(List<Toy> toys, bool LHand, NoteCutInfo data)
        {
            if (Plugin.Control.HitCounter >= 15)
            {
                Plugin.Control.MissCounter = Math.Max(--Plugin.Control.MissCounter, 0);
                Plugin.Control.HitCounter = 0;
                foreach (Toy toy in toys)
                {
                    if (toy.IsConnected() && toy.IsActive())
                    {
                        toy.vibrate(0, Plugin.Control.MissCounter);
                    }
                }
            }
            this.HandleMiss(toys, LHand);
        }

        public override void HandleMiss(List<Toy> toys, bool LHand)
        {
            Plugin.Control.HitCounter = 0;

            Plugin.Control.MissCounter = Math.Min(Plugin.Control.MissCounter, 20);

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
                    toy.vibratePreset(3, true);
                }
            }
        }
        public override void HandleFireworks(List<Toy> toys)
        {

        }

        public override string GetModusName()
        {
            return "Challenge 1";
        }

        public override List<string> getUiElements()
        {
            return new List<string> { "fireworksBtn", "presetOnBombHit", "presetBombSlider" };
        }

        public override string getDescription()
        {
            return "Vibrate missing boxes. Each time level go up, after 15 hits, level goes one step down";
        }

    }
}
