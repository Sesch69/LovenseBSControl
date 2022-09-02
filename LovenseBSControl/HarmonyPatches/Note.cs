using HarmonyLib;
using LovenseBSControl.Configuration;
using System;

namespace LovenseBSControl.HarmonyPatches
{


    [HarmonyPatch(typeof(NoteController))]
    [HarmonyPatch("SendNoteWasCutEvent", MethodType.Normal)]
    internal static class SendNoteWasCutEvent
    {
        private static void Postfix(in NoteCutInfo noteCutInfo, NoteData ____noteData, NoteController __instance)
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.HandleCut(noteCutInfo.saberType == SaberType.SaberA, noteCutInfo.allIsOK, noteCutInfo);
            }
        }
    }

    [HarmonyPatch(typeof(NoteController))]
    [HarmonyPatch("SendNoteWasMissedEvent", MethodType.Normal)]
    internal static class SendNoteWasMissedEvent
    {
        private static void Postfix(NoteData ____noteData, NoteController __instance)
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.HandleCut(____noteData.colorType.ToString().Equals("ColorA"), false);
            }
        }
    }
}
