using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LovenseBSControl.Classes.Modus
{
    abstract class Modus : DefaultModus
    {
        
        public abstract override void HandleHit(List<Toy> toys, bool LHand, NoteCutInfo data);
        public abstract override void HandleMiss(List<Toy> toys, bool LHand);
        public abstract override void HandleBomb(List<Toy> toys);
        public abstract override void HandleFireworks(List<Toy> toys);

        public abstract override string GetModusName();

        public abstract override List<string> getUiElements();

        public abstract override string getDescription();

        public abstract override bool useLastLevel();

    }
}
