using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    public class Character
    {

        public Game1 Game { get; set; }
        public string Name { get; set; }
        public int CurrentHitpoints { get; set; }
        public int MaxHitpoints { get; set; }
        public int Armor { get; set; }
        public int Attack { get; set; }
        public int Damage { get; set; }

        public Tile currentPosition { get; set; }
        public Texture2D texture { get; set; }
        public List<SoundEffect> fxList { get; set; }
        public enum Behavior { Wandering }
        public Behavior behavior { get; set; }
        private bool isPlayer = false;
        private Random rnd;
        public float rotation = 0;


        public Character(Game1 Game,  string Name, int hitpoints, int armor, int attack, int damage, Texture2D texture, Tile currentPosition, List<SoundEffect> fxList, Random rnd)
        {
            this.Game = Game;
            this.Name = Name;
            this.CurrentHitpoints = hitpoints;
            this.MaxHitpoints = hitpoints;
            this.Armor = armor;
            this.Attack = attack;
            this.Damage = damage;
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

                    foundTile = true;

                    if ((player.currentPosition.gridX == newX && player.currentPosition.gridY == newY))
                    {
                        this.AttackCharacter(this.Game.player);
                        fxList[0].Play();
                        foundTile = false;
                    }

                    foreach(Character e in enemyList)
                    {
                        if((e.currentPosition.gridX == newX && e.currentPosition.gridY == newY))
                        {
                            foundTile = false;
                            if(this.isPlayer)
                            {
                                this.AttackCharacter(e);
                            }
                        }
                    }

                    if (foundTile)
                    {
                        this.currentPosition = t;
                    }


                }
             }
            if (!foundTile && this.isPlayer)
            {
                fxList[0].Play();
            }
            if (newX < curX)
            {
                this.rotation = 1.57079632679F;
            }
            if (newX > curX)
            {
                this.rotation = 4.71238898038F;
            }
            if (newY < curY)
            {
                this.rotation = 3.14159265359F;
            }
            if (newY > curY)
            {
                this.rotation = 0;
            }
        }

        public void AttackCharacter(Character attacked)
        {
            int attackRoll = this.rnd.Next(1, 21);
            if(attackRoll > attacked.Armor)
            {
                attacked.CurrentHitpoints = attacked.CurrentHitpoints - this.Damage;
                this.Game.textList.Add(new Text("Hit! " + this.Damage + " damage", (this.Game.GraphicsDevice.Viewport.Width / 2) + (this.currentPosition.gridX * 50) - (this.Game.player.currentPosition.gridX * 50), (this.Game.GraphicsDevice.Viewport.Height / 2) + (this.currentPosition.gridY * 50) - (this.Game.player.currentPosition.gridY * 50), 0.01F, 0, -0.5F));
            }
            else
            {
                this.Game.textList.Add(new Text("Miss!", (this.Game.GraphicsDevice.Viewport.Width / 2) + (this.currentPosition.gridX * 50) - (this.Game.player.currentPosition.gridX * 50), (this.Game.GraphicsDevice.Viewport.Height / 2) + (this.currentPosition.gridY * 50) - (this.Game.player.currentPosition.gridY * 50), 0.01F, 0, -0.5F));
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
