using System;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LovenseBSControl.Configuration
{
    public class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public bool Enabled { get; set; } = true;

        public bool VibrateMiss { get; set; } = true;
        public bool VibrateHit { get; set; } = false;

        public int IntenseHit { get; set; } = 10;
        public int IntenseMiss { get; set; } = 10;

        public bool RandomIntenseHit { get; set; } = false;
        public bool RandomIntenseMiss { get; set; } = false;

        public int DurationHit { get; set; } = 500;
        public int DurationMiss { get; set; } = 500;

        public int PresetBomb { get; set; } = 2;

        public bool DefaultConnection { get; set; } = true;

        public bool LocalHostConnection { get; set; } = false;

        public const string baseUrl = "https://127-0-0-1.lovense.club";
        public const string localHost = "127.0.0.1";
        public const string basePort = "30010";

        public string ipAdress = "";

        public string port { get; set; } = "30010";

        public string modus { get; set; } = "Default";

        public int Rotate { get; set; } = 1;

        public int Air { get; set; } = 1;

        public bool VibeBombs { get; set; } = true;

        [UseConverter]
        public virtual Dictionary<string, ToysConfig> ToyConfigurations { get; set; } = new Dictionary<string, ToysConfig>();

        public void AddToyConfiguration(string Id,  ToysConfig ToyConfiguration)
        {
            if (!IsAdded(Id))
            {
                ToyConfigurations.Add(Id,ToyConfiguration);
            }
        }

        public ToysConfig getToyConfig(string Id) 
        {
            return ToyConfigurations[Id];
        }

        public bool IsAdded(string Id)
        {
            return ToyConfigurations.ContainsKey(Id);
        }
        
    }
}
