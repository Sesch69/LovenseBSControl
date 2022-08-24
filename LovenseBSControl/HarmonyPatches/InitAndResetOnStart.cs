using HarmonyLib;
using LovenseBSControl.Configuration;

namespace LovenseBSControl.HarmonyPatches
{
    [HarmonyPatch(typeof(ScoreController), "Start")]
    class InitAndResetOnStart
    {
        static void Prefix()
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.HitCounter = 0;
                Plugin.Control.MissCounter = 0;
            }
            
            LovenseBSControlController.Instance.BindCutMissEvents();
        }
    }
}
