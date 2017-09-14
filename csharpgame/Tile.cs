using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace csharpgame
{
    class Tile
    {
        public enum Type { Dirt }
        public Type type { get; set; }
        public Texture2D texture { get; set; }
        public Vector2 gridPos { get; set; }
        public Character onboard { get; set; }

        public Tile(Type type,Texture2D texture,Vector2 gridPos)
        {
            this.type = type;
            this.gridPos = gridPos;
            this.texture = texture;
        }
    }
}
