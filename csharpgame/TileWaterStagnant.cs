using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class TileWaterStagnant : Tile
    {
        public static Texture2D Texture { get; set; }
        public TileWaterStagnant (int x, int y)
        {
            this.type = Tile.Type.Liquid;
            this.texture = Texture;
            this.gridX = x;
            this.gridY = y;
        }
    }
}
