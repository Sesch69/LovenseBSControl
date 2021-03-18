using HarmonyLib;
using LovenseBSControl.Configuration;

namespace LovenseBSControl.HarmonyPatches
{


    [HarmonyPatch(typeof(ObstacleSaberSparkleEffectManager), "BurnMarkPosForSaberType")]
    class BurnMarkPosForSaberType
    {
        static void Prefix(SaberType saberType)
        {
            Plugin.Log.Notice("CONTACT!");
            Plugin.Log.Notice(saberType.ToString());
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.handleBomb();
            }
            //return true;
        }
    }
}
