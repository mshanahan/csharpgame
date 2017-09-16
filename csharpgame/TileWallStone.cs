using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class TileWallStone : Tile
    {
        public static Texture2D Texture { get; set; }
        public TileWallStone(int x, int y)
        {
            this.type = Tile.Type.Floor;
            this.texture = Texture;
            this.gridX = x;
            this.gridY = y;
        }
    }
}
