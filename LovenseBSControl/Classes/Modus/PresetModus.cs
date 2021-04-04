using LovenseBSControl.Configuration;
using System.Collections.Generic;

namespace LovenseBSControl.Classes.Modus
{
    class PresetModus : Modus
    {
        public List<string> UiElements = new List<string>{ "vibrateOnMissBtn" , "vibrateOnHitBtn" };

        public override void HandleHit(List<Toy> toys, bool LHand, NoteCutInfo data)
        {
            foreach (Toy toy in toys)
            {
                if (toy.IsConnected() && toy.IsActive() && toy.CheckHand(LHand))
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
                    toy.vibratePreset(PluginConfig.Instance.PresetBomb);
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
        public override void HandleFireworks(List<Toy> toys)
        {

        }

        public override string GetModusName()
        {
            return "Preset";
        }

        public override List<string> getUiElements()
        {
            return new List<string> { "presetOnBombHit", "presetBombSlider", "fireworksBtn" };
        }

        public override string getDescription()
        {
            return "Vibrate on missing boxes with a preset";
        }

    }
}
