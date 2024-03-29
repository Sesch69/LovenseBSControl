﻿using HarmonyLib;
using LovenseBSControl.Configuration;

namespace LovenseBSControl.HarmonyPatches
{
    [HarmonyPatch(typeof(FireworkItemController), "Fire")]
    class Fire
    {
        static void Prefix()
        {
            if (PluginConfig.Instance.Enabled )
            {
                Plugin.Control.HandleFireworks();
            }
        }
    }
}
