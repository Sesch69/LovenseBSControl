using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using LovenseBSControl.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace LovenseBSControl.Classes
{
    class Request
    {
        private static readonly HttpClient client = new HttpClient();

        public Request()
        {
        }

        public async Task<List<Toy>> RequestToysListAsync()
        {
            List<Toy> Toys = new List<Toy>();

            foreach (var connection in PluginConfig.Instance.GetActiveConnections())
            {
                var baseUrl = connection.Value.CreateBaseUrl();
                var getToysUrl = baseUrl + "/GetToys";

                try
                {
                    if (!connection.Key.Equals("Default"))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    }

                    Plugin.Log.Debug($"[{connection.Value.Name}] Trying {getToysUrl}...");

                    var responseString = await client.GetStringAsync(getToysUrl);
                    JObject toysString = JObject.Parse(responseString);
                    
                    if (!toysString["type"].ToString().ToLower().Equals("ok"))
                    {
                        Plugin.Log.Warn($"[{connection.Value.Name}] Connect sent non-OK response: {responseString}");
                        continue;
                    }

                    foreach (JProperty dataToy in (JToken) toysString["data"])
                    {
                        JToken toyDetails = dataToy.Value;
                        Toy newToy = new Toy(toyDetails["id"].ToString().ToUpper(), toyDetails["name"].ToString(),
                            toyDetails["status"].ToString().Equals("1"), toyDetails["version"].ToString(),
                            toyDetails["nickName"].ToString());
                        if (toyDetails["battery"] is null || toyDetails["battery"].ToString().Equals(""))
                        {
                            newToy.SetBattery(0);
                        }
                        else
                        {
                            newToy.SetBattery(Int32.Parse(toyDetails["battery"].ToString()));
                        }

                        newToy.SetConnection(connection.Key);
                        Toys.Add(newToy);
                        
                        Plugin.Log.Info($"[{connection.Value.Name}] Toy added! (name={toyDetails["name"]}, status={toyDetails["status"]}, battery={toyDetails["battery"]})");

                        await UseToy(newToy, 1, 10);
                    }
                }

                catch (HttpRequestException e)
                {
                    Plugin.Log.Error($"[{connection.Value.Name}] HTTP error trying {getToysUrl}: {e.Message}");
                }
            }

            return Toys;
        }

        public async Task updateBattery(Toy toy)
        {
            foreach (var connection in PluginConfig.Instance.GetActiveConnections())
            {
                try
                {
                    if (!connection.Key.Equals("Default"))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    }

                    var responseString =
                        await client.GetStringAsync(connection.Value.CreateBaseUrl() + "/Battery?t=" + toy.GetId());
                    JObject toysString = JObject.Parse(responseString);

                    if (!toysString["type"].ToString().ToLower().Equals("ok"))
                    {
                        Plugin.Log.Warn($"[{connection.Value.Name}] Connect sent non-OK response: {responseString}");
                    }

                    if (toysString["data"] is null || toysString["data"].ToString().Equals(""))
                    {
                        toy.SetBattery(0);
                    }
                    else
                    {
                        toy.SetBattery(Int32.Parse(toysString["data"].ToString()));
                    }
                }
                catch (HttpRequestException e)
                {
                    Plugin.Log.Error($"[{connection.Value.Name}] HTTP error for battery check: {e.Message}");
                }
            }
        }

        public async Task TestToy(Toy toy)
        {
            if(PluginConfig.Instance.LovenseConnectAPI == 1) { 
                await this.VibrateToy(toy, PluginConfig.Instance.DurationHit, PluginConfig.Instance.IntenseHit);
            }
            else { 
                RequestData data = new RequestData(toy, PluginConfig.Instance.DurationHit, PluginConfig.Instance.IntenseHit);
                foreach (var connection in PluginConfig.Instance.GetActiveConnections()) {
                    if (toy.GetConnection().Equals(connection.Key)) { 
                        Plugin.Log.Notice(JsonConvert.SerializeObject(data));
                        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        var result = await client.PostAsync(connection.Value.CreateBaseUrl() + "/command", content);
                        Plugin.Log.Notice(result.ToString());
                    }
                }
            }
        }

        public async Task UseToy(Toy toy, int time, int level)
        {
            Plugin.Log.Info($"UseToy (Toy={toy.GetText()}, time={time}, level={level})");
            await this.VibrateToy(toy, time, level);
        }

        public async Task VibrateToy(Toy toy, int delay = 0, int level = 5)
        {
            toy.setOn();
            await StartToy(toy, level);
            if (delay > 0)
            {
                await Task.Delay(delay);
                toy.setOff();
                await StopToy(toy);
            }
        }

        public async Task PresetToy(Toy toy, int time, int preset = 2, bool resume = false)
        {
            await this.VibratePresetToy(toy, preset);
            if (resume)
            {
                await Task.Delay(4000);
                toy.resume();
            }
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
            foreach (var connection in PluginConfig.Instance.GetActiveConnections())
            {
                if (toy.GetConnection().Equals(connection.Key))
                {
                    if (!connection.Key.Equals("Default"))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    }

                    await client.GetStringAsync(connection.Value.CreateBaseUrl() + "/Vibrate" + toy.GetMotor() + "?v=" + level + "&t=" + toy.GetId());
                    if (toy.CanRotate() && PluginConfig.Instance.Rotate > 0)
                    {
                        await client.GetStringAsync(connection.Value.CreateBaseUrl() + "/Rotate" + toy.GetMotor() + "?v=" + PluginConfig.Instance.Rotate + "&t=" + toy.GetId());
                    }
                    if (toy.CanPump() & PluginConfig.Instance.Air > 0)
                    {
                        await client.GetStringAsync(connection.Value.CreateBaseUrl() + "/AirAuto" + toy.GetMotor() + "?v=" + PluginConfig.Instance.Air + "&t=" + toy.GetId());
                    }
                }
            }
        }
        public async Task StartPresetToy(Toy toy, int preset = 0)
        {
            foreach (var connection in PluginConfig.Instance.GetActiveConnections())
            {
                if (toy.GetConnection().Equals(connection.Key))
                {
                    await client.GetStringAsync(connection.Value.CreateBaseUrl() + "/Preset?v=" + preset + "&t=" + toy.GetId());
                }
            }
        }

        public async Task StopToy(Toy toy)
        {
            foreach (var connection in PluginConfig.Instance.GetActiveConnections())
            {
                if (toy.GetConnection().Equals(connection.Key))
                {
                    if (!connection.Key.Equals("Default"))
                    {
                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    }

                    await client.GetStringAsync(connection.Value.CreateBaseUrl() + "/Vibrate" + toy.GetMotor() + "?v=0&t=" + toy.GetId());
                    if (toy.CanRotate())
                    {
                        await client.GetStringAsync(connection.Value.CreateBaseUrl() + "/Rotate?v=0&t=" + toy.GetId());
                    }
                    if (toy.CanPump())
                    {
                        await client.GetStringAsync(connection.Value.CreateBaseUrl() + "/AirAuto" + toy.GetMotor() + "?v=0&t=" + toy.GetId());
                    }
                }
            }
        }

    }

    class RequestData
    {
        public string command = "Pattern";
        public string rule = "";
        public string strength = "";
        public double timeSec = 0.1;
        public string toy = "";
        public int apiVer = 1;

        public RequestData(Toy toy, float time, int level)
        {
            this.rule = "V:1;F:v;S:" + time + "#";
            this.timeSec = 1;
            this.toy = toy.GetId();
            this.strength = level.ToString();
        }
    }
}
