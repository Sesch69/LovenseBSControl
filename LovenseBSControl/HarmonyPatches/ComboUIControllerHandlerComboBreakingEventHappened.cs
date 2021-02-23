using HarmonyLib;
using IPA.Utilities;
using LovenseBSControl.Configuration;

namespace LovenseBSControl
{
    [HarmonyPatch(typeof(ComboUIController), "HandleComboBreakingEventHappened")]
    class HandleComboBreakingEventHappened
    {
        static bool Prefix(ComboUIController __instance)
        {
            if (PluginConfig.Instance.Enabled)
            {
                /*
                Plugin.Control.vibrateActive();
                Plugin.Log.Notice("Debug1");
                if (!__instance.GetField<bool, ComboUIController>("_comboLost"))
                {
                    Plugin.Log.Notice("Debug2");
                }
                */
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(ScoreController), "HandleNoteWasCut")]
    class HandleNoteWasCut
    {
        static bool Prefix(ScoreController __instance)
        {
            if (PluginConfig.Instance.Enabled && PluginConfig.Instance.VibrateHit)
            {
                Plugin.Control.vibrateActive();
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(ScoreController), "HandleNoteWasMissed")]
    class HandleNoteWasMissed
    {
        static bool Prefix(ScoreController __instance)
        {
            if (PluginConfig.Instance.Enabled && PluginConfig.Instance.VibrateMiss)
            {
                Plugin.Control.vibrateActive();
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
            if (PluginConfig.Instance.Enabled)
            {
                Plugin.Control.vibrateActive();
                return false;
            }
            return true;
        }
    }
}