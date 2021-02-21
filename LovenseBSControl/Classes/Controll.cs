using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IPALogger = IPA.Logging.Logger;

namespace LovenseBSControl.Classes
{
    class Controll
    {
        List<Toy> toys = new List<Toy>();

        private Classes.Request Request;
        public Controll() {
            this.Request = new Classes.Request();
        }

        public void init() { 
        }

        public async Task loadToysAsync(IPALogger Log) {

            await this.Request.requestToysListAsync(Log);

        }

        public async Task testToy(Toy toy) {

            await this.Request.testToy(toy);
        
        }
    }
}
