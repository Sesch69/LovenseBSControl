using HarmonyLib;
using IPA;
using IPA.Config.Stores;
using LovenseBSControl.Configuration;
using LovenseBSControl.UI;
using IPALogger = IPA.Logging.Logger;
using System.Reflection;
using UnityEngine;
using BeatSaberMarkupLanguage.Settings;
using BS_Utils.Utilities;

namespace LovenseBSControl
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Harmony harmony;
        internal static IPALogger Log { get; private set; }

        internal static Classes.Control Control { get; private set; }

        [Init]
        public Plugin(IPALogger logger, IPA.Config.Config conf)
        {
            Log = logger;
            Log.Info("LovenseBSControl initialized.");

            PluginConfig.Instance = conf.Generated<PluginConfig>();

            CheckConnections();

            Control = new Classes.Control();
            Control.LoadToysAsync().ConfigureAwait(true);
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            new GameObject("LovenseBSControlController").AddComponent<LovenseBSControlController>();
            BSEvents.gameSceneActive += GameCutAction;
            harmony = new Harmony("com.Sesch69.BeatSaber.LovenseBSControl");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            BSMLSettings.instance.AddSettingsMenu("Lovense BS Control", "LovenseBSControl.UI.SettingsView.bsml", SettingsViewController.instance);

        }

        void GameCutAction()
        {
            if (PluginConfig.Instance.Enabled) LovenseBSControlController.Instance.GetControllers();
        }


        [OnExit]
        public void OnApplicationQuit()
        {
            Control.stopActive();
            harmony.UnpatchAll("com.CyanSnow.BeatSaber.LovenseBSControl");
            BSMLSettings.instance.RemoveSettingsMenu("Lovense BS Control");
            BSEvents.gameSceneActive -= GameCutAction;
            Log.Debug("OnApplicationQuit");

        }

        private void CheckConnections()
        {
            if (!PluginConfig.Instance.ConnectionExist("Default")) 
            {
                PluginConfig.Instance.AddConnectionConfiguration(ConnectionConfig.CreateDefaultConnection()).SetStatus(true);
            }
            if (!PluginConfig.Instance.ConnectionExist("Localhost"))
            {
                PluginConfig.Instance.AddConnectionConfiguration( ConnectionConfig.CreatLocalHostConnection());
            }
        }
    }
}
