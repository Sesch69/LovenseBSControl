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
using System.Linq;
using UnityEngine.SceneManagement;
using LovenseBSControl.Classes;

namespace LovenseBSControl
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Harmony harmony;
        internal static IPALogger Log { get; private set; }

        internal static Classes.Control Control { get; private set; }

        private BeatmapObjectSpawnController SpawnController;

        private BatteryElement BatteryElement;

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
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            harmony = new Harmony("com.Sesch69.BeatSaber.LovenseBSControl");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            BSMLSettings.instance.AddSettingsMenu("Lovense BS Control", "LovenseBSControl.UI.SettingsView.bsml", SettingsViewController.instance);

        }

        void GameCutAction()
        {
            if (PluginConfig.Instance.Enabled) LovenseBSControlController.Instance.GetControllers();
        }

        private void SceneManagerOnActiveSceneChanged(Scene oldScene, Scene newScene)
        {
            if (newScene.name == "GameCore")
            {
                if (SpawnController == null)
                    SpawnController = Resources.FindObjectsOfTypeAll<BeatmapObjectSpawnController>().FirstOrDefault();
                if (SpawnController == null) return;

                if (PluginConfig.Instance.BattteryShow && Plugin.Control.IsAToyActive())
                {
                    BatteryElement = new GameObject("BatteryElement").AddComponent<BatteryElement>();
                }
            }

        }


        [OnExit]
        public void OnApplicationQuit()
        {
            Control.StopActive();
            harmony.UnpatchAll("com.CyanSnow.BeatSaber.LovenseBSControl");
            BSMLSettings.instance.RemoveSettingsMenu("Lovense BS Control");
            BSEvents.gameSceneActive -= GameCutAction;
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
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
