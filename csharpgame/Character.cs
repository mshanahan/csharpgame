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
        public enum Behavior { Wandering }

        public string Name { get; set; }
        public int CurrentHitpoints { get; set; }
        public int MaxHitpoints { get; set; }
        public int Armor { get; set; }
        public int Attack { get; set; }
        public int Damage { get; set; }
        public Tile currentPosition { get; set; }
        public Texture2D texture { get; set; }
        public Texture2D DeathTexture { get; set; }

        public Behavior behavior { get; set; }
        private bool isPlayer = false;
        public float rotation = 0;


        public Character(string Name, int hitpoints, int armor, int attack, int damage, Texture2D texture, Texture2D DeathTexture, Tile currentPosition)
        {
            this.Name = Name;
            this.CurrentHitpoints = hitpoints;
            this.MaxHitpoints = hitpoints;
            this.Armor = armor;
            this.Attack = attack;
            this.Damage = damage;
            this.texture = texture;
            this.DeathTexture = DeathTexture;
            this.currentPosition = currentPosition;
        }

        public void setPlayer()
        {
            this.isPlayer = true;
        }

        public void Move(int xDiff, int yDiff)
        {
            Environment env = Environment.Current();
            int curX = currentPosition.gridX;
            int curY = currentPosition.gridY;

            int newX = curX + xDiff;
            int newY = curY + yDiff;

            bool foundTile = false;
            foreach (Tile t in env.TileList)
            {
                if (t.gridX == newX && t.gridY == newY)
                {

                    foundTile = true;

                    if ((env.Player.currentPosition.gridX == newX && env.Player.currentPosition.gridY == newY))
                    {
                        this.AttackCharacter(env.Game.player);
                        env.SoundFXList[0].Play();
                        foundTile = false;
                    }

                    foreach (Character e in env.NPCList)
                    {
                        if ((e.currentPosition.gridX == newX && e.currentPosition.gridY == newY))
                        {
                            foundTile = false;
                            if (this.isPlayer)
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
                env.SoundFXList[0].Play();
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
            Environment env = Environment.Current();
            int attackRoll = env.Random.Next(1, 21);
            if (attackRoll > attacked.Armor)
            {
                attacked.CurrentHitpoints = attacked.CurrentHitpoints - this.Damage;
                env.Add(new Text("Hit! " + this.Damage + " damage", (env.Game.GraphicsDevice.Viewport.Width / 2) + (this.currentPosition.gridX * 50) - (env.Game.player.currentPosition.gridX * 50), (env.Game.GraphicsDevice.Viewport.Height / 2) + (this.currentPosition.gridY * 50) - (env.Game.player.currentPosition.gridY * 50), 0.01F, 0, -0.5F));
            }
            else
            {
                env.Add(new Text("Miss!", (env.Game.GraphicsDevice.Viewport.Width / 2) + (this.currentPosition.gridX * 50) - (env.Game.player.currentPosition.gridX * 50), (env.Game.GraphicsDevice.Viewport.Height / 2) + (this.currentPosition.gridY * 50) - (env.Game.player.currentPosition.gridY * 50), 0.01F, 0, -0.5F));
            }
        }

        public void KillCharacter(Character c)
        {
            Environment env = Environment.Current();
            Corpse newCorpse = new Corpse(this.texture, this.currentPosition, this.rotation);
        }

        public void AIRoutine()
        {
            Environment env = Environment.Current();

            //Wandering behavior
            if (this.behavior == Behavior.Wandering)
            {
                int movementDirection = env.Random.Next(0, 4);
                if (movementDirection == 0)
                {
                    this.Move(1, 0);
                }
                if (movementDirection == 1)
                {
                    this.Move(-1, 0);
                }
                if (movementDirection == 2)
                {
                    this.Move(0, 1);
                }
                if (movementDirection == 3)
                {
                    this.Move(0, -1);
                }
            }
        }
    }
}
