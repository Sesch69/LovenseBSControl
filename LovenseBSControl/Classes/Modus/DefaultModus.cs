using LovenseBSControl.Configuration;
using System;
using System.Collections.Generic;


namespace LovenseBSControl.Classes.Modus
{
    class DefaultModus
    {

        public virtual void HandleHit(List<Toy> toys, bool LHand, NoteCutInfo data)
        {
            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive() && PluginConfig.Instance.VibrateHit)
                {
                    if (toy.CheckHand(LHand) && !toy.GetToyConfig().Random)
                    {
                        toy.vibrate(PluginConfig.Instance.DurationHit);
                    }

                    if (toy.GetToyConfig().Random)
                    {
                        Random rng = new Random();
                        bool random = rng.Next(0, 2) > 0;
                        if ((random && LHand) || (!random && !LHand))
                        {
                            toy.vibrate( PluginConfig.Instance.DurationHit);
                        }
                    }
                }
            }
        }

        public virtual void HandleMiss(List<Toy> toys, bool LHand)
        {
            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive() && PluginConfig.Instance.VibrateMiss)
                {
                    if (toy.CheckHand(LHand) && !toy.GetToyConfig().Random)
                    {
                        toy.vibrate(PluginConfig.Instance.DurationMiss, false);
                    }

                    if (toy.GetToyConfig().Random)
                    {
                        Random rng = new Random();
                        bool random = rng.Next(0, 2) > 0;
                        if ((random && LHand) || (!random && !LHand))
                        {
                            toy.vibrate(PluginConfig.Instance.DurationMiss, false);
                        }
                    }
                }
            }
        }

        public virtual void HandleBomb(List<Toy> toys)
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

        public virtual void HandleFireworks(List<Toy> toys)
        {
            if (!PluginConfig.Instance.Fireworks) return;

            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive())
                {
                    toy.vibratePreset(PluginConfig.Instance.PresetBomb);
                }
            }
        }

        public virtual string GetModusName()
        {
            return "Default";
        }

        public virtual List<string> getUiElements()
        {
            return new List<string> { "vibrateOnMissBtn", "vibrateOnHitBtn", "randomIntenseMissBtn", "intenseMissSlider", "durationMissSlider", "randomIntenseHitBtn", "intenseHitSlider", "durationHitSlider", "presetOnBombHit", "presetBombSlider", "fireworksBtn" };
        }

        public virtual string getDescription()
        {
            return "Default modus, free configuration for hit and miss boxes, also bombs behavior and fireworks.";
        }

        public virtual bool useLastLevel()
        {
            return false;
        }


    }
}
