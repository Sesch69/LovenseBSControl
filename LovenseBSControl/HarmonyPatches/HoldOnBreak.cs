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
                if (PluginConfig.Instance.BreakPunishment)
                {
                    Plugin.Control.PunishmentBreak();
                }
                else
                {
                    Plugin.Control.PauseGame();
                }
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
                 Plugin.Control.ResumeGame();
            }
        }
    }
}