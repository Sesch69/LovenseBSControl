using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using LovenseBSControl.Configuration;
using LovenseBSControl.Classes;
using HMUI;
using UnityEngine;
using System.Threading.Tasks;

namespace LovenseBSControl.UI
{
    internal class SettingsViewController : PersistentSingleton<SettingsViewController>
	{

		[UIValue("enabled")]
		public bool Enabled
		{
			get
			{
				return PluginConfig.Instance.Enabled;
			}
			set
			{
				PluginConfig.Instance.Enabled = value;
			}
		}

		[UIValue("vibrateMiss")]
		public bool VibrateMiss
		{
			get
			{
				return PluginConfig.Instance.VibrateMiss;
			}
			set
			{
				PluginConfig.Instance.VibrateMiss = value;
			}
		}

        [UIValue("vibrateBombHit")]
        public bool VibrateBomb
        {
            get
            {
                return PluginConfig.Instance.VibeBombs;
            }
            set
            {
                PluginConfig.Instance.VibeBombs = value;
            }
        }

        [UIValue("intense")]
        public int Intense
        {
            get
            {
                return PluginConfig.Instance.Intense;
            }
            set
            {
                PluginConfig.Instance.Intense = value;
            }
        }

        [UIValue("duration")]
        public int Duration
        {
            get
            {
                return PluginConfig.Instance.Duration;
            }
            set
            {
                PluginConfig.Instance.Duration = value;
            }
        }

        [UIValue("rotate")]
        public int Rotate
        {
            get
            {
                return PluginConfig.Instance.Rotate;
            }
            set
            {
                PluginConfig.Instance.Rotate = value;
            }
        }

        [UIValue("air")]
        public int Air
        {
            get
            {
                return PluginConfig.Instance.Air;
            }
            set
            {
                PluginConfig.Instance.Air = value;
            }
        }

        [UIValue("vibrateHit")]
		public bool VibrateHit
		{
			get
			{
				return PluginConfig.Instance.VibrateHit;
			}
			set
			{
				PluginConfig.Instance.VibrateHit = value;
			}
		}
        [UIComponent("toy-list")]
        public CustomListTableData customListTableData = null;


        private async Task ShowToys(int delay = 0)
        {
            if (delay > 0) await Task.Delay(delay);
            SetupList();
        }

        [UIAction("clicked-refresh")]
        private async void ClickedReloadAll()
        {
            await Plugin.Control.LoadToysAsync();

            await ShowToys();
        }
        [UIAction("clicked-lHand")]
        private void ClickedLHand()
        {
            Plugin.Log.Notice("LHand clicked");
        }

        [UIAction("clicked-rHand")]
        private void ClickedRHand()
        {
            Plugin.Log.Notice("RHand clicked");
        }

        [UIAction("clicked-deactivate")]
        private void ClickedDeactivate()
        {
            Plugin.Log.Notice("Deactivate clicked");
        }

        [UIAction("#post-parse")]
        public void SetupList()
        {
            List<Toy> Toys = Plugin.Control.getToyList();
            customListTableData.data.Clear();
            foreach (Toy toy in Toys)
            {
                string path = "img/ambi";

                Sprite sprite = Resources.Load<Sprite>(path);
                CustomListTableData.CustomCellInfo customCellInfo = new CustomListTableData.CustomCellInfo(toy.getNickName(), toy.getText() + " - " + (toy.IsConnected() ? "Connected" : "Disconnected"), sprite);
                customListTableData.data.Add(customCellInfo);
            }

            customListTableData.tableView.ReloadData();
            int selectedMaterial = 1;

            customListTableData.tableView.ScrollToCellWithIdx(selectedMaterial, TableViewScroller.ScrollPositionType.Beginning, false);
            customListTableData.tableView.SelectCellWithIdx(selectedMaterial);
        }


        [UIAction("toySelect")]
        public void Select(TableView _, int row)
        {
            List<Toy> Toys = Plugin.Control.getToyList();
            Toy selectedToy = Toys[row];
            if (selectedToy.IsConnected())
            {
                selectedToy.test();
            }
        }
    }
}
