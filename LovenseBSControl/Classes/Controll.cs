using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovenseBSControl.Classes
{
    class Controll
    {
        public Controll() { 
        
        }

        public void init() { 
        }

        public void loadToys() {
            var request = new Request();
            request.requestToysListAsync();


        }
    }
}
