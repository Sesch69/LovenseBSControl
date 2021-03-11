using HarmonyLib;
using IPA.Utilities;
using LovenseBSControl.Configuration;
using UnityEngine;

namespace LovenseBSControl
{
    [HarmonyPatch(typeof(BombNoteController), "HandleWasCutBySaber")]
    class HandleWasCutBySaber
    {
        static void Prefix(BombNoteController __instance)
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.handleBomb();
            }
            //return true;
        }
    }
}