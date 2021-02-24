using HarmonyLib;
using IPA.Utilities;
using LovenseBSControl.Configuration;

namespace LovenseBSControl
{

    [HarmonyPatch(typeof(ScoreController), "HandleNoteWasCut")]
    class HandleNoteWasCut
    {
        
        static bool Prefix(NoteController noteController, NoteCutInfo noteCutInfo)
        {
            if (PluginConfig.Instance.Enabled && PluginConfig.Instance.VibrateHit)
            {
                Plugin.Control.HitCounter++;
                Plugin.Control.vibrateActive(noteController.noteData.colorType.ToString().Equals("ColorA"));
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(ScoreController), "HandleNoteWasMissed")]
    class HandleNoteWasMissed
    {
        static bool Prefix(NoteController noteController)
        {
            if (PluginConfig.Instance.Enabled && PluginConfig.Instance.VibrateMiss)
            {
                Plugin.Control.MissCounter++;
                Plugin.Control.vibrateActive(noteController.noteData.colorType.ToString().Equals("ColorA"));
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(BombNoteController), "HandleWasCutBySaber")]
    class HandleWasCutBySaber
    {
        static bool Prefix(BombNoteController __instance)
        {
            if (PluginConfig.Instance.Enabled && PluginConfig.Instance.VibeBombs)
            {
                Plugin.Control.vibrateActivePreset();
                return false;
            }
            return true;
        }
    }


}