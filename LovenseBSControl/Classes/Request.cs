using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using LovenseBSControl.Configuration;

using IPALogger = IPA.Logging.Logger;


namespace LovenseBSControl.Classes
{
    class Request
    {
        private static readonly HttpClient client = new HttpClient();
        private String baseUrl = "";

        public Request(String baseUrl = "https://127-0-0-1.lovense.club", String port = "30010" ) {
            this.baseUrl = baseUrl + ":" + port;
        }

        public async Task<List<Toy>> requestToysListAsync()
        {
            List<Toy> Toys = new List<Toy>();
            try
            {
                var responseString = await client.GetStringAsync(baseUrl + "/GetToys");
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

        public async Task StartToy(Toy toy, int level)
        {
            await client.GetStringAsync(baseUrl + "/Vibrate" + toy.GetMotor() + "?v=" + level + "&t=" + toy.GetId());
            if (toy.canRotate() && PluginConfig.Instance.Rotate > 0)
            {
                await client.GetStringAsync(baseUrl + "/Rotate" + toy.GetMotor() + "?v=" + level + "&t=" + toy.GetId());
            }

        }

        
        public async Task StopToy(Toy toy)
        {   
            await client.GetStringAsync(baseUrl + "/Vibrate" + toy.GetMotor() + "?v=0&t=" + toy.GetId());
            if (toy.canRotate()) {
                await client.GetStringAsync(baseUrl + "/Rotate?v=0&t=" + toy.GetId());
            }
        }
    }
}
