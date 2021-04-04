using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace LovenseBSControl.Classes
{
    public class BatteryElement : MonoBehaviour
    {
        private TextMeshProUGUI BattersStatus;


        private void Awake()
        {
            try
            {   
                Init();
            }
            catch (Exception ex)
            {
                Plugin.Log.Notice(ex.ToString());
            }
        }


        void Init()
        {
            Canvas canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            CanvasScaler cs = gameObject.AddComponent<CanvasScaler>();
            cs.scaleFactor = 10.0f;
            cs.dynamicPixelsPerUnit = 10f;
            GraphicRaycaster gr = gameObject.AddComponent<GraphicRaycaster>();
            gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);

            BattersStatus = gameObject.AddComponent<TextMeshProUGUI>();
            BattersStatus.rectTransform.SetParent(canvas.transform as RectTransform, false);
            BattersStatus.rectTransform.anchoredPosition = new Vector2(0, 0);
            BattersStatus.text = this.CreateText(); 
            BattersStatus.alignment = TextAlignmentOptions.Left;
            BattersStatus.transform.localScale *= .12f;
            BattersStatus.fontSize = 1.8f;
            BattersStatus.color = Color.white;
            BattersStatus.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1f);
            BattersStatus.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1f);
            BattersStatus.enableWordWrapping = false;
            BattersStatus.transform.localPosition = new Vector3(6.0f, 3.5f, 8f);
        }

        public void UpdateBattery()
        {
            BattersStatus.text = this.CreateText();
        }

        private string CreateText()
        {
            string text = "Battery status";
            foreach (Toy toy in Plugin.Control.GetToyList())
            {
                if (toy.IsConnected() && toy.IsActive())
                {
                    Plugin.Control.updateBattery(toy);
                    text += "\r\n" + toy.GetNickName() + " " + toy.getBattery() + "%";
                }
            }
            return text;
        }
    }
}
