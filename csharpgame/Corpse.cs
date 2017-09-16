using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class Corpse
    {
        public Texture2D Texture { get; set;  }
        public Tile Position { get; set; }
        public float Rotation { get; set; }

        public Corpse(Texture2D Texture, Tile Position, float Rotation)
        {
            this.Texture = Texture;
            this.Position = Position;
            this.Rotation = Rotation;
        }
    }
}
