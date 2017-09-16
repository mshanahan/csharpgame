﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace csharpgame
{
    public class Tile
    {
        public enum Type { Dirt, Rock }
        public Type type { get; set; }
        public Texture2D texture { get; set; }
        //public Vector2 gridPos { get; set; }
        public int gridX { get; set; }
        public int gridY { get; set; }

        public Tile(Type type, Texture2D texture, int gridX, int gridY)
        {
            this.type = type;
            this.gridX = gridX;
            this.gridY = gridY;
            this.texture = texture;
        }

        public static int distanceBetween(Tile a, Tile b)
        {
            double distance = Math.Sqrt( Math.Pow(b.gridX - a.gridX,2) + Math.Pow(b.gridY - a.gridY,2) );
            return (int) distance;
        }
    }
}
