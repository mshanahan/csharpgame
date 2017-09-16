using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class TileFloorStone : Tile
    {
        public static List<Texture2D> Textures { get; set; }
        public TileFloorStone(int x, int y)
        {
            Environment env = Environment.Current();
            this.type = Tile.Type.Floor;
            this.texture = Textures[env.Random.Next(0, Textures.Count)];
            this.gridX = x;
            this.gridY = y;
        }
    }
}
