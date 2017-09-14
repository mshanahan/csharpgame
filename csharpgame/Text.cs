using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class Text
    {

        public string Contents { get; set; }
        public float XPos { get; set; }
        public float YPos { get; set; }
        public float Transparency { get; set; }
        public float decayRate { get; set; }
        public float xMovement { get; set; }
        public float yMovement { get; set; }

        public Text(string Contents, int XPos, int YPos, float decayRate, float xMovement, float yMovement)
        {
            this.Contents = Contents;
            this.XPos = XPos;
            this.YPos = YPos;
            this.Transparency = 1.0F;
            this.decayRate = decayRate;
            this.xMovement = xMovement;
            this.yMovement = yMovement;
        }
    }
}
