using System;

namespace LovenseBSControl.Configuration
{
    public class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public bool Enabled { get; set; } = true;

        public bool VibrateMiss { get; set; } = true;

        public bool VibrateHit { get; set; } = false;

        public int Intense { get; set; } = 10;

        public int Duration { get; set; } = 500;

        public String baseUrl { get; set; } = "https://127-0-0-1.lovense.club";

        public String port { get; set; } = "30010";

        public int Rotate { get; set; } = 1;

        public int Air { get; set; } = 1;

        public bool VibeBombs { get; set; } = true;

    }

    
}
