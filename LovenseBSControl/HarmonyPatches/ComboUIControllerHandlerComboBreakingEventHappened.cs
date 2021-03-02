using HarmonyLib;
using IPA.Utilities;
using LovenseBSControl.Configuration;

namespace LovenseBSControl
{

    [HarmonyPatch(typeof(ScoreController), "HandleNoteWasCut")]
    class HandleNoteWasCut
    {
        
        static void Prefix(NoteController noteController, NoteCutInfo noteCutInfo)
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.handleCut(noteController.noteData.colorType.ToString().Equals("ColorA"), noteCutInfo.allIsOK);
            }
        }
    }

    [HarmonyPatch(typeof(ScoreController), "HandleNoteWasMissed")]
    class HandleNoteWasMissed
    {
        static void  Prefix(NoteController noteController)
        {
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.handleCut(noteController.noteData.colorType.ToString().Equals("ColorA"), false);
            }
        }
    }

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