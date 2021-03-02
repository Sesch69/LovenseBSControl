using HarmonyLib;
using LovenseBSControl.Configuration;

namespace LovenseBSControl.HarmonyPatches
{

    [HarmonyPatch(typeof(ScoreController), "OnDestroy")]
    class OnDestroy
    {
        static void Prefix()
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.endGame();
            }
        }
    }
}

