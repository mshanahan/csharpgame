using System;
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
        public enum Type { Wall, Floor, Liquid }
        public Type type { get; set; }
        public Texture2D texture { get; set; }
        public int gridX { get; set; }
        public int gridY { get; set; }
        public bool Draw { get; set; }

        public Tile()
        {

        }

        public static int distanceBetween(Tile a, Tile b)
        {
            double distance = Math.Sqrt(Math.Pow(b.gridX - a.gridX, 2) + Math.Pow(b.gridY - a.gridY, 2));
            return (int)distance;
        }

        public static Tile FindTile(int x, int y)
        {
            Environment env = Environment.Current();
            return env.TileList[x][y];
        }

        public static bool CheckVisibility(int x, int y)
        {
            Tile t = Tile.FindTile(x, y);
            if (t == null || (t != null && t.type == Type.Wall))
            {
                if(t != null) t.Draw = true;
                return false;
            }
            t.Draw = true;
            return true;
        }

        private static void Swap<T>(ref T lhs, ref T rhs) { T temp; temp = lhs; lhs = rhs; rhs = temp; }

        /// <summary>
        /// The plot function delegate
        /// </summary>
        /// <param name="x">The x co-ord being plotted</param>
        /// <param name="y">The y co-ord being plotted</param>
        /// <returns>True to continue, false to stop the algorithm</returns>
        public delegate bool PlotFunction(int x, int y);

        //https://stackoverflow.com/questions/11678693/all-cases-covered-bresenhams-line-algorithm
        public static void Line(int x, int y, int x2, int y2, PlotFunction plot)
        {
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                if(!plot(x, y)) return;
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }
    }
}
