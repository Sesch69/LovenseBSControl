using LovenseBSControl.Configuration;
using System;
using System.Collections.Generic;

namespace LovenseBSControl.Classes.Modus
{
    class Challenge2Modus : Modus
    {
        public override string GetModusName()
        {
            return "Speed Cut";
        }

        public override void HandleBomb(List<Toy> toys)
        {
            if (!PluginConfig.Instance.VibeBombs) return;

            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive())
                {
                    toy.vibratePreset(PluginConfig.Instance.PresetBomb);
                }
            }
        }

        public override void HandleHit(List<Toy> toys, bool LHand, NoteCutInfo data)
        {
            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive() && toy.CheckHand(LHand))
                {
                    //Plugin.Log.Notice("Speed: " + data.saberSpeed.ToString() + " Rating Counter: " + data.swingRatingCounter.beforeCutRating + " - " + data.swingRatingCounter.afterCutRating + " Saber Dir: " + data.saberDir);
                    int level = Math.Min(20, (int)Math.Round(data.saberSpeed / 3));
                    toy.vibrate(PluginConfig.Instance.DurationHit, level);
                }
            }
        }
        public override void HandleFireworks(List<Toy> toys)
        {
            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive())
                {
                    toy.vibratePreset(PluginConfig.Instance.PresetBomb);
                }
            }
        }

        public override void HandleMiss(List<Toy> toys, bool LHand)
        {
            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive() && toy.CheckHand(LHand))
                {
                    toy.vibrate(PluginConfig.Instance.DurationMiss, false);
                }
            }
        }
        
        public override List<string> getUiElements()
        {
            return new List<string> { "vibrateOnHitBtn", "vibrateOnMissBtn", "randomIntenseMissBtn", "intenseMissSlider", "presetOnBombHit", "presetBombSlider", "fireworksBtn", "durationHitSlider" };
        }

        public override string getDescription()
        {
            return "Vibrate on cutting boxes with level of speed of the cuts";
        }

    }
}
