using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    public class Text
    {

        public string Contents { get; set; }
        public float XPos { get; set; }
        public float YPos { get; set; }
        public float Transparency { get; set; }
        public float DecayRate { get; set; }
        public float XMovement { get; set; }
        public float YMovement { get; set; }
        public bool Kill { get; set; }

        public Text(string Contents, int XPos, int YPos, float decayRate, float xMovement, float yMovement)
        {
            this.Contents = Contents;
            this.XPos = XPos;
            this.YPos = YPos;
            this.Transparency = 1.0F;
            this.DecayRate = decayRate;
            this.XMovement = xMovement;
            this.YMovement = yMovement;
            this.Kill = false;
        }

        public void Decay()
        {
            this.Transparency = this.Transparency - DecayRate;
            this.XPos = this.XPos + XMovement;
            this.YPos = this.YPos + YMovement;
            if (this.Transparency <= 0)
            {
                this.Kill = true;
            }
        }
    }
}
