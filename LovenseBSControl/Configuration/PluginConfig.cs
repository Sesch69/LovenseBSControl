using System;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using System.Collections.Generic;

namespace LovenseBSControl.Configuration
{
    public class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public bool Enabled { get; set; } = true;

        public bool VibrateMiss { get; set; } = true;

        public bool VibrateHit { get; set; } = false;

        public int Intense { get; set; } = 10;

        public bool RandomIntense { get; set; } = false;

        public int Duration { get; set; } = 500;

        public bool DefaultConnection { get; set; } = true;

        public bool LocalHostConnection { get; set; } = false;

        public const String baseUrl = "https://127-0-0-1.lovense.club";
        public const String localHost = "127.0.0.1";
        public const String basePort = "30010";

        public String ipAdress = "";

        public String port { get; set; } = "30010";

        public int Rotate { get; set; } = 1;

        public int Air { get; set; } = 1;

        public bool VibeBombs { get; set; } = true;
        
        [UseConverter(typeof(ListConverter<ToysConfig>))]
        public List<ToysConfig> ToyConfigurations { get; set; } = new List<ToysConfig>();

        public void AddToyConfiguration(ToysConfig ToyConfiguration)
        {
            if (!IsAdded(ToyConfiguration))
            {
                ToyConfigurations.Add(ToyConfiguration);
            }
        }

        public bool IsAdded(ToysConfig ToyConfiguration)
        {
            return ToyConfigurations.Contains(ToyConfiguration);
        }
        
    }

    
}
