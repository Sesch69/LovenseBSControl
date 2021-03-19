using HarmonyLib;
using LovenseBSControl.Configuration;

namespace LovenseBSControl.HarmonyPatches
{
    [HarmonyPatch(typeof(FireworkItemController), "Fire")]
    class Fire
    {
        static void Prefix()
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Log.Notice("FIREWORKS");
                Plugin.Control.handleBomb();
                Plugin.Control.handleBomb();
            }
        }
    }
}
