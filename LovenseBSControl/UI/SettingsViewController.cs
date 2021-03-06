using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using System.Collections.Generic;
using LovenseBSControl.Configuration;
using LovenseBSControl.Classes;
using HMUI;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using BeatSaberMarkupLanguage.Components.Settings;
using System.Reflection;
using System.IO;

namespace LovenseBSControl.UI
{
    internal class SettingsViewController : PersistentSingleton<SettingsViewController>
	{

        private Toy selectedToy = null;
        private int selectedToyNumber = 0;

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

        [UIValue("randeomIntense")]
        public bool RandeomIntense
        {
            get
            {
                return PluginConfig.Instance.RandomIntense;
            }
            set
            {
                PluginConfig.Instance.RandomIntense = value;
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

        [UIValue("defaultConnection")]
        public bool DefaultConnection
        {
            get
            {
                return PluginConfig.Instance.DefaultConnection;
            }
            set
            {
                statusPort.gameObject.SetActive(!value);
                statusIpAdress.gameObject.SetActive(!value);
                statusLocalHost.gameObject.SetActive(!value);
                PluginConfig.Instance.DefaultConnection = value;
            }
        }

        [UIObject("defaultLocalHost")]
        private GameObject statusLocalHost;

        [UIValue("defaultLHConnection")]
        public bool DefaultLHConnection
        {
            get
            {
                return PluginConfig.Instance.LocalHostConnection;
            }
            set
            {
                PluginConfig.Instance.LocalHostConnection = value;
            }
        }

        [UIObject("portInput")]
        private GameObject statusPort;

        [UIValue("port")]
        public string Port
        {
            get
            {
                return PluginConfig.Instance.port;
            }
            set
            {
                PluginConfig.Instance.port = value;
            }
        }

        [UIObject("ipAdressInput")]
        private GameObject statusIpAdress;

        [UIValue("ipAdress")]
        public string IpAdress
        {
            get
            {
                return PluginConfig.Instance.ipAdress;
            }
            set
            {
                PluginConfig.Instance.ipAdress = value;
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
        
        [UIAction("clicked-test")]
        private void ClickedTest()
        {
            if (this.selectedToy.IsConnected())
            {
                this.selectedToy.test();
            }
        }

        private string settedSelection = HTypes.bHands;

        [UIValue("listChoice")]
        public string ListChoice
        { 
            get
            {
                if (this.selectedToy != null)
                {
                    settedSelection = this.selectedToy.getToyConfig().HType; 
                    return settedSelection;
                }
                return HTypes.bHands;
            }
            set
            {
                settedSelection = value;
            }
        }

        [UIValue("modusChoice")]
        public string ModusChoice
        {
            get
            {
                return PluginConfig.Instance.modus;
            }
            set
            {
                PluginConfig.Instance.modus = value;
                
            }
        }

        [UIComponent("toyConfigSetting")]
        public GenericSetting choice;

        [UIComponent("toyConfigSetting")]
        public DropDownListSetting text;

        [UIAction("onChangeHands")]
        public void onChangeHands(string data) {
            if (this.selectedToy != null)
            {
                ToysConfig config = this.selectedToy.getToyConfig();
                config.setHType(data);
                SetupList();
            }
        }

        [UIValue("list-options")]
        private List<object> options = new object[] { HTypes.bHands, HTypes.lHand, HTypes.rHand, HTypes.random, HTypes.inactive}.ToList();

        [UIValue("modus-options")]
        private List<object> modi = Plugin.Control.AvailableModi;

        [UIAction("#post-parse")]
        public void SetupList()
        {

            statusPort.gameObject.SetActive(!PluginConfig.Instance.DefaultConnection);
            statusIpAdress.gameObject.SetActive(!PluginConfig.Instance.DefaultConnection);
            statusLocalHost.gameObject.SetActive(!PluginConfig.Instance.DefaultConnection);

            if (!Plugin.Control.isToyAvailable()) {
                return;
            }
            List<Toy> Toys = Plugin.Control.getToyList();
            customListTableData.data.Clear();
           
            foreach (Toy toy in Toys)
            {
                Plugin.Log.Notice("LovenseBSControl.Resources.Sprites." + toy.GetPictureName());
                Sprite sprite = Utilities.LoadSpriteFromResources("LovenseBSControl.Resources.Sprites." + toy.GetPictureName());
               

                ToysConfig toyConfig = toy.getToyConfig();
                CustomListTableData.CustomCellInfo customCellInfo = new CustomListTableData.CustomCellInfo(toy.getNickName(), toy.getText() + " - " + ((toy.IsConnected() ? "Connected" : "Disconnected") + (toy.IsConnected()? " - " + toy.getBattery() + "%" : "") + " - " + toyConfig.HType), sprite);
                customListTableData.data.Add(customCellInfo);
            }

            customListTableData.tableView.ReloadData();
            this.selectedToy = Toys[this.selectedToyNumber];

            customListTableData.tableView.ScrollToCellWithIdx(this.selectedToyNumber, TableViewScroller.ScrollPositionType.Beginning, false);
            customListTableData.tableView.SelectCellWithIdx(this.selectedToyNumber);
        }


        [UIAction("toySelect")]
        public void Select(TableView _, int row)
        {
            List<Toy> Toys = Plugin.Control.getToyList();
            this.selectedToyNumber = row;
            this.selectedToy = Toys[row];
            choice.updateOnChange = true;
            choice.associatedValue.SetValue(this.selectedToy.getToyConfig().HType);
            text.Value = this.selectedToy.getToyConfig().HType;
        }

        [UIAction("#apply")]
        public void OnApply()
        {
            Plugin.Control.setModus();
        }

    }
}
