using LovenseBSControl.Configuration;
using System.Collections.Generic;

namespace LovenseBSControl.Classes.Modus
{
    class PresetModus : Modus
    {

        public override void HandleHit(List<Toy> toys, bool LHand, bool success)
        {
            if (!success)
            {
                foreach (Toy toy in toys)
                {
                    if (toy.IsConnected() && toy.IsActive() && toy.CheckHand(LHand))
                    {
                        toy.vibratePreset(2);
                    }
                }
            }
        }

        public override void HandleMiss(List<Toy> toys, bool LHand)
        {
            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive() && toy.CheckHand(LHand))
                {
                    toy.vibratePreset(2);
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
                    toy.vibratePreset(3);
                }
            }
        }

        public override string GetModusName()
        {
            return "Preset";
        }
    }
}
