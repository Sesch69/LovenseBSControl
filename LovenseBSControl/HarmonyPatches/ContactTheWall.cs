using HarmonyLib;
using LovenseBSControl.Configuration;

namespace LovenseBSControl.HarmonyPatches
{


    [HarmonyPatch(typeof(ObstacleSaberSparkleEffectManager), "Start")]
    class BurnMarkPosForSaberType
    {
        static void Prefix()
        {
            Plugin.Log.Notice("CONTACT!");
            //Plugin.Log.Notice(saberType.ToString());
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.handleBomb();
            }
            //return true;
        }
    }
}
