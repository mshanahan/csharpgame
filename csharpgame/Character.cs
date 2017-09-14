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

        public Character(int ac,int hp,int move,Tile currentPosition)
        {
            this.armorClass = ac;
            this.hitpoints = hp;
            this.moveSpeed = move;
            this.currentPosition = currentPosition;
        }

        public void Move(List<Tile> tileList, int xDiff, int yDiff)
        {
            int curX = currentPosition.gridX;
            int curY = currentPosition.gridY;

            int newX = curX + xDiff;
            int newY = curX + yDiff;

            Predicate < Tile > pred = doesTileExistAtPoint;

            bool doesTileExistAtPoint(Tile obj)
            {
                return obj.gridX == newX && obj.gridY == newY;
            }

            Tile newTile = tileList.Find(pred);
            if(newTile != null)
            {
                this.currentPosition = newTile;
            }
        }
    }
}
