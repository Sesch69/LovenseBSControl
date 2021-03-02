using HarmonyLib;
using LovenseBSControl.Configuration;

namespace LovenseBSControl.HarmonyPatches
{
    [HarmonyPatch(typeof(GamePause))]
    [HarmonyPatch("Pause")]
    class HoldOnPause
    {
        static void Prefix()
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.pauseGame();
            }

        }
    }

    [HarmonyPatch(typeof(GamePause))]
    [HarmonyPatch("Resume")]
    class EndPause
    {
        static void Prefix()
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.resumeGame();
            }

        }
    }
}