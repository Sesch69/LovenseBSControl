using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace LovenseBSControl.Classes
{
    class Request
    {
        private static readonly HttpClient client = new HttpClient();

        public Array Toys;

        public Request() { 
        
        }

        public async Task requestToysListAsync()
        {
            var responseString = await client.GetStringAsync("https://127-0-0-1.lovense.club:30010/GetToys");
            JObject toys = JObject.Parse(responseString);

            if (!toys["type"].Equals("ok")) {
                return;
            }

            var length = toys["data"];

            foreach (var toy in toys) { 
                
            }
        }
    }
}
