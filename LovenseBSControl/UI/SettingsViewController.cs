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
using UnityEngine.UI;

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
                randomIntenseMissBtn.gameObject.SetActive(value);
                intenseMissSlider.gameObject.SetActive(value && !PluginConfig.Instance.RandomIntenseMiss);
                durationMissSlider.gameObject.SetActive(value);
                resetVertical();
                PluginConfig.Instance.VibrateMiss = value;
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
                randomIntenseHitBtn.gameObject.SetActive(value);
                intenseHitSlider.gameObject.SetActive(value && !PluginConfig.Instance.RandomIntenseHit);
                durationHitSlider.gameObject.SetActive(value);
                resetVertical();
                PluginConfig.Instance.VibrateHit = value;
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
                presetBombSlider.gameObject.SetActive(value);
                PluginConfig.Instance.VibeBombs = value;
            }
        }

        [UIObject("randomIntenseHitBtn")]
        private GameObject randomIntenseHitBtn;


        [UIValue("randomIntenseHit")]
        public bool RandomIntenseHit
        {
            get
            {
                return PluginConfig.Instance.RandomIntenseHit;
            }
            set
            {
                intenseHitSlider.gameObject.SetActive(!value);
                PluginConfig.Instance.RandomIntenseHit = value;
            }
        }

        [UIObject("randomIntenseMissBtn")]
        private GameObject randomIntenseMissBtn;

        [UIValue("randomIntenseMiss")]
        public bool RandeomIntenseMiss
        {
            get
            {
                return PluginConfig.Instance.RandomIntenseMiss;
            }
            set
            {
                intenseMissSlider.gameObject.SetActive(!value);
                PluginConfig.Instance.RandomIntenseMiss = value;
            }
        }

        [UIObject("intenseHitSlider")]
        private GameObject intenseHitSlider;

        [UIValue("intenseHit")]
        public int IntenseHit
        {
            get
            {
                return PluginConfig.Instance.IntenseHit;
            }
            set
            {
                PluginConfig.Instance.IntenseHit = value;
            }
        }

        [UIObject("intenseMissSlider")]
        private GameObject intenseMissSlider;

        [UIValue("intenseMiss")]
        public int IntenseMiss
        {
            get
            {
                return PluginConfig.Instance.IntenseMiss;
            }
            set
            {
                PluginConfig.Instance.IntenseMiss = value;
            }
        }

        [UIObject("durationHitSlider")]
        private GameObject durationHitSlider;

        [UIValue("durationHit")]
        public int DurationHit
        {
            get
            {
                return PluginConfig.Instance.DurationHit;
            }
            set
            {
                PluginConfig.Instance.DurationHit = value;
            }
        }

        [UIObject("durationMissSlider")]
        private GameObject durationMissSlider;

        [UIValue("durationMiss")]
        public int DurationMiss
        {
            get
            {
                return PluginConfig.Instance.DurationMiss;
            }
            set
            {
                PluginConfig.Instance.DurationMiss = value;
            }
        }

        [UIObject("vibrationPresetSlider")]
        private GameObject presetBombSlider;

        [UIValue("presetNumber")]
        public int presetNumber
        {
            get
            {
                return PluginConfig.Instance.PresetBomb;
            }
            set
            {
                PluginConfig.Instance.PresetBomb = value;
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
            if (this.selectedToy != null && this.selectedToy.IsConnected())
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
                    settedSelection = this.selectedToy.GetToyConfig().HType; 
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
                ToysConfig config = this.selectedToy.GetToyConfig();
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

            randomIntenseMissBtn.gameObject.SetActive(PluginConfig.Instance.VibrateMiss);
            intenseMissSlider.gameObject.SetActive(PluginConfig.Instance.VibrateMiss && !PluginConfig.Instance.RandomIntenseMiss);
            durationMissSlider.gameObject.SetActive(PluginConfig.Instance.VibrateMiss);

            randomIntenseHitBtn.gameObject.SetActive(PluginConfig.Instance.VibrateHit);
            intenseHitSlider.gameObject.SetActive(PluginConfig.Instance.VibrateHit && !PluginConfig.Instance.RandomIntenseHit);
            durationHitSlider.gameObject.SetActive(PluginConfig.Instance.VibrateHit);

            presetBombSlider.gameObject.SetActive(PluginConfig.Instance.VibeBombs);

            if (!Plugin.Control.isToyAvailable()) {
                return;
            }
            List<Toy> Toys = Plugin.Control.getToyList();
            customListTableData.data.Clear();
           
            foreach (Toy toy in Toys)
            {
                Sprite sprite = Utilities.LoadSpriteFromResources("LovenseBSControl.Resources.Sprites." + toy.GetPictureName());
                ToysConfig toyConfig = toy.GetToyConfig();
                CustomListTableData.CustomCellInfo customCellInfo = new CustomListTableData.CustomCellInfo(toy.getNickName(), toy.getText() + " - " + ((toy.IsConnected() ? "Connected" : "Disconnected") + (toy.IsConnected()? " - " + toy.getBattery() + "%" : "") + " - " + toyConfig.HType), sprite);
                customListTableData.data.Add(customCellInfo);
            }

            customListTableData.tableView.ReloadData();
            this.selectedToy = Toys[this.selectedToyNumber];
            
            customListTableData.tableView.ScrollToCellWithIdx(this.selectedToyNumber, TableView.ScrollPositionType.Beginning, false);
            customListTableData.tableView.SelectCellWithIdx(this.selectedToyNumber);
        }


        [UIAction("toySelect")]
        public void Select(TableView _, int row)
        {
            List<Toy> Toys = Plugin.Control.getToyList();
            this.selectedToyNumber = row;
            this.selectedToy = Toys[row];
            choice.updateOnChange = true;
            choice.associatedValue.SetValue(this.selectedToy.GetToyConfig().HType);
            text.Value = this.selectedToy.GetToyConfig().HType;
        }

        [UIAction("#apply")]
        public void OnApply()
        {
            Plugin.Control.setModus();
        }

        [UIComponent("verticalElement")]
        private LayoutElement verticalElement;

        private void resetVertical()
        {
            int childCount = 0;
            for (int i = 0; i < verticalElement.transform.childCount; i++)
            {
                if (verticalElement.transform.GetChild(i).gameObject.activeSelf) {
                    childCount++;
                }
            }
            verticalElement.minHeight = childCount * 7;
        }
    }
}
