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
        public enum Behavior { Wandering }
        public Behavior behavior { get; set; }
        private bool isPlayer = false;
        private Random rnd;
        public float rotation = 0;


        public Character(int ac, int hp, int move, Texture2D texture, Tile currentPosition, List<SoundEffect> fxList, Random rnd)
        {
            this.armorClass = ac;
            this.hitpoints = hp;
            this.moveSpeed = move;
            this.texture = texture;
            this.currentPosition = currentPosition;
            this.fxList = fxList;
            this.rnd = rnd;
        }

        public void setPlayer()
        {
            this.isPlayer = true;
        }

        public void Move(List<Tile> tileList, int xDiff, int yDiff, Character player, List<Character> enemyList)
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
                    if(newX < curX)
                    {
                        this.rotation = 1.57079632679F;
                    }
                    if(newX > curX)
                    {
                        this.rotation = 4.71238898038F;
                    }
                    if(newY < curY)
                    {
                        this.rotation = 3.14159265359F;
                    }
                    if(newY > curY)
                    {
                        this.rotation = 0;
                    }
                }
             }
            if (!foundTile && this.isPlayer)
            {
                fxList[0].Play();
            }
        }

        public void AIRoutine(List<Tile> tileList, Character player, List<Character> enemyList)
        {
            //Wandering behavior
            if(this.behavior == Behavior.Wandering)
            {
                int movementDirection = rnd.Next(0, 4);
                if(movementDirection == 0)
                {
                    this.Move(tileList, 1, 0, player, enemyList);
                }
                if (movementDirection == 1)
                {
                    this.Move(tileList, -1, 0, player, enemyList);
                }
                if (movementDirection == 2)
                {
                    this.Move(tileList, 0, 1, player, enemyList);
                }
                if (movementDirection == 3)
                {
                    this.Move(tileList, 0, -1, player, enemyList);
                }
            }
        }
    }
}
