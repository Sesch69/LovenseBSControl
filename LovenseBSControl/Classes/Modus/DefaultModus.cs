using LovenseBSControl.Configuration;
using System;
using System.Collections.Generic;


namespace LovenseBSControl.Classes.Modus
{
    class DefaultModus
    {

        public virtual void HandleHit(List<Toy> toys, bool LHand, bool success)
        {
            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive() && (PluginConfig.Instance.VibrateHit && success) || (PluginConfig.Instance.VibrateMiss && !success))
                {
                    if (toy.CheckHand(LHand) && !toy.GetToyConfig().Random)
                    {
                        toy.vibrate(success?PluginConfig.Instance.DurationHit: PluginConfig.Instance.DurationMiss);
                    }

                    if (toy.GetToyConfig().Random)
                    {
                        Random rng = new Random();
                        bool random = rng.Next(0, 2) > 0;
                        if ((random && LHand) || (!random && !LHand))
                        {
                            toy.vibrate(success ? PluginConfig.Instance.DurationHit : PluginConfig.Instance.DurationMiss);
                        }
                    }
                }
            }
        }

        public virtual void HandleMiss(List<Toy> toys, bool LHand)
        {
            this.HandleHit(toys, LHand, false);
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

        public virtual string GetModusName()
        {
            return "Default";
        }

    }
}
