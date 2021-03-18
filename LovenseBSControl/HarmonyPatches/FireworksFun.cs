using HarmonyLib;
using LovenseBSControl.Configuration;

namespace LovenseBSControl.HarmonyPatches
{
    [HarmonyPatch(typeof(FireworksController), "HandleFireworkItemControllerDidFinish")]
    class HandleFireworkItemControllerDidFinish
    {
        static void Prefix(FireworkItemController fireworkItemController)
        {
            Plugin.Log.Notice("FIREWORKS");
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.handleBomb();
            }
            //return true;
        }
    }
}
