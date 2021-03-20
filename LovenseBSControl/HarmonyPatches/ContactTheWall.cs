using HarmonyLib;
using LovenseBSControl.Configuration;

namespace LovenseBSControl.HarmonyPatches
{


    [HarmonyPatch(typeof(ObstacleSaberSparkleEffectManager), "Start")]
    class BurnMarkPosForSaberType
    {
        static void Prefix()
        {
            
            if (PluginConfig.Instance.Enabled)
            {
                //Plugin.Control.handleBomb();
            }
        }
    }
}
