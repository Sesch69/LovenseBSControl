using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using System.Collections.Generic;
using LovenseBSControl.Configuration;
using LovenseBSControl.Classes;
using HMUI;
using UnityEngine;
using System.Threading.Tasks;

namespace LovenseBSControl.UI
{
    internal class SettingsViewController : PersistentSingleton<SettingsViewController>
	{

        private Toy selectedToy = null;
        private bool rightHand;

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
            this.selectedToy.switchLHand();
        }

        [UIAction("clicked-rHand")]
        private void ClickedRHand()
        {
            this.selectedToy.switchRHand();
        }

        [UIAction("clicked-deactivate")]
        private void ClickedDeactivate()
        {
            Plugin.Log.Notice("Deactivate clicked");
        }

        [UIAction("clicked-test")]
        private void ClickedTest()
        {
            if (this.selectedToy.IsConnected())
            {
                this.selectedToy.test();
            }
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
            int selectedToy = 0;
            this.selectedToy = Toys[selectedToy];

            customListTableData.tableView.ScrollToCellWithIdx(selectedToy, TableViewScroller.ScrollPositionType.Beginning, false);
            customListTableData.tableView.SelectCellWithIdx(selectedToy);
        }


        [UIAction("toySelect")]
        public void Select(TableView _, int row)
        {
            List<Toy> Toys = Plugin.Control.getToyList();
            this.selectedToy = Toys[row];
        }
    }
}
