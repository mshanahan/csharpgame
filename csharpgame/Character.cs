using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class Character
    {
        public int armorClass { get; set; }
        public int hitpoints { get; set; }
        public int moveSpeed { get; set; }
        public Tile currentPosition { get; set; }
        public Texture2D texture { get; set; }
        public List<SoundEffect> fxList { get; set; }
        

        public Character(int ac, int hp, int move, Texture2D texture, Tile currentPosition,List<SoundEffect> fxList)
        {
            this.armorClass = ac;
            this.hitpoints = hp;
            this.moveSpeed = move;
            this.texture = texture;
            this.currentPosition = currentPosition;
            this.fxList = fxList;
        }

        public void Move(List<Tile> tileList, int xDiff, int yDiff)
        {
            int curX = currentPosition.gridX;
            int curY = currentPosition.gridY;

            int newX = curX + xDiff;
            int newY = curY + yDiff;

            bool foundTile = false;
            foreach (Tile t in tileList)
            {
                 if(t.gridX == newX && t.gridY == newY)
                {
                    this.currentPosition = t;
                    foundTile = true;
                }
             }
            if (!foundTile)
            {
                fxList[0].Play();
            }

        }
    }
}
