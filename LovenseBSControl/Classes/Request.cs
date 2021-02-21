using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

using IPALogger = IPA.Logging.Logger;


namespace LovenseBSControl.Classes
{
    class Request
    {
        private static readonly HttpClient client = new HttpClient();
        private const String baseUrl = "https://127-0-0-1.lovense.club:30010";

        public Request() {
            

        }

        public async Task<List<Toy>> requestToysListAsync(IPALogger Log)
        {
            List<Toy> Toys = new List<Toy>();
            var responseString = await client.GetStringAsync(baseUrl + "/GetToys");
            JObject toysString = JObject.Parse(responseString);

            if (!toysString["type"].ToString().Equals("ok")) {
                Log.Info("Lovense Connect not active/running.");
                return Toys;
            }

            foreach (JProperty dataToy in (JToken)toysString["data"] )
            {
                Log.Info("Add toy");
                JToken toyDetails = dataToy.Value;
                Toy newToy = new Toy(toyDetails["id"].ToString(), toyDetails["name"].ToString(), toyDetails["status"].ToString().Equals("1"), toyDetails["nickName"].ToString());
                Toys.Add(newToy);
            }
            return Toys;
        }
        
        public async Task testToy(Toy toy) {
            await this.vibrateToy(toy, 3000, 5, toy.getMotor());
        }

        public async Task vibrateToy(Toy toy, int time, int level, String differentMotor = "")
        {
            //differentMotor 1 / 2
            await client.GetStringAsync(baseUrl + "/Vibrate"+ differentMotor + "?v=" + level + "&t=" + toy.getId());
            System.Threading.Thread.Sleep(time);
            await client.GetStringAsync(baseUrl + "/Vibrate" + differentMotor + "?v=0&t=" + toy.getId());

        }
    }
}
