using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using LovenseBSControl.Configuration;
using System.Net;

namespace LovenseBSControl.Classes
{
    class Request
    {
        private static readonly HttpClient client = new HttpClient();

        public Request( ) {
        }

        public async Task<List<Toy>> requestToysListAsync()
        {
            List<Toy> Toys = new List<Toy>();
            try
            {
                if (!PluginConfig.Instance.DefaultConnection)
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                }
                var responseString = await client.GetStringAsync(getBaseUrl() + "/GetToys");
                JObject toysString = JObject.Parse(responseString);

                if (!toysString["type"].ToString().Equals("ok"))
                {
                    Plugin.Log.Info("Lovense Connect not active/running.");
                    return Toys;
                }

                foreach (JProperty dataToy in (JToken)toysString["data"])
                {
                    JToken toyDetails = dataToy.Value;
                    Toy newToy = new Toy(toyDetails["id"].ToString(), toyDetails["name"].ToString(), toyDetails["status"].ToString().Equals("1"), toyDetails["version"].ToString(), toyDetails["nickName"].ToString());
                    Toys.Add(newToy);
                }
            }
            catch (HttpRequestException e)
            {
                
                Plugin.Log.Info("Lovense Connect not reachable.");
                Plugin.Log.Info(e.ToString());
            }
            return Toys;
        }
        
        public async Task TestToy(Toy toy) {
            await this.VibrateToy(toy, PluginConfig.Instance.Duration, PluginConfig.Instance.Intense);
        }

        public async Task UseToy(Toy toy, int time, int level)
        {
            await this.VibrateToy(toy, time, level);
        }

        public async Task VibrateToy(Toy toy, int delay = 0, int level = 5)
        {
            toy.setOn();
            await StartToy(toy, level);
            if (delay > 0) await Task.Delay(delay);
            toy.setOff();
            await StopToy(toy);
        }

        public async Task PresetToy(Toy toy, int time, int preset = 2)
        {
            await this.VibratePresetToy(toy, preset);
        }

        public async Task VibratePresetToy(Toy toy, int preset = 2)
        {
            toy.setOn();
            await StartPresetToy(toy, preset);
            await Task.Delay(2000);
            toy.setOff();
            await StopToy(toy);
        }

        public async Task StartToy(Toy toy, int level)
        {
            await client.GetStringAsync(getBaseUrl() + "/Vibrate" + toy.GetMotor() + "?v=" + level + "&t=" + toy.GetId());
            if (toy.canRotate() && PluginConfig.Instance.Rotate > 0)
            {
                await client.GetStringAsync(getBaseUrl() + "/Rotate" + toy.GetMotor() + "?v=" + level + "&t=" + toy.GetId());
            }

        }
        public async Task StartPresetToy(Toy toy, int preset = 0)
        {
            await client.GetStringAsync(getBaseUrl() + "/Preset?v=" + preset + "&t=" + toy.GetId());
        }
        
        public async Task StopToy(Toy toy)
        {   
            await client.GetStringAsync(getBaseUrl() + "/Vibrate" + toy.GetMotor() + "?v=0&t=" + toy.GetId());
            if (toy.canRotate()) {
                await client.GetStringAsync(getBaseUrl() + "/Rotate?v=0&t=" + toy.GetId());
            }
        }


        private String getBaseUrl() {

            if (PluginConfig.Instance.DefaultConnection)
            {
                return PluginConfig.Instance.baseUrl + ":30010";
            }
            else
            {
                return "https://" + PluginConfig.Instance.ipAdress + ":" + PluginConfig.Instance.port;
            }
        }

        



    }
}
