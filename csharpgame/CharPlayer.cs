using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    public class CharPlayer : Character
    {
        private static CharPlayer Player { get; set; }
        public static Texture2D PlayerImage { get; set; }
        public static Texture2D PlayerDeathImage { get; set; }
        public static int MaxTicks { get; } = 360;
        public bool Locked { get; set; }
        public int Gold { get; set; }
        public int TorchCount { get; set; }
        public int TorchTicks { get; set; }

        public CharPlayer(Tile t) : base (t)
        {
            Environment env = Environment.Current();
            this.Name = "Player";
            this.setPlayer();
            this.CurrentHitpoints = 10;
            this.MaxHitpoints = CurrentHitpoints;
            this.Armor = 0;
            this.Attack = 0;
            this.Damage = 0;
            this.texture = PlayerImage;
            this.DeathTexture = PlayerDeathImage;
            this.Locked = false;
            this.Gold = 0;
            this.TorchCount = 1;
            this.TorchTicks = 0;
            this.ItemArmor = new ItemArmor("Clothing", 0, 10);
            this.ItemWeapon = new ItemWeapon("Stick", 0, 1, 3);
        }

        public static CharPlayer GetPlayer()
        {
            if(Player == null)
            {
                Environment env = Environment.Current();
                //foreach(Tile t in env.TileList)
                //{
                //    if(t.type == Tile.Type.Floor)
                //    {
                //        Player = new CharPlayer(t);
                //        break;
                //    }
                //}

                bool PlayerSpawned = false;
                bool TraderSpawned = false;
                for(int i=0;i<env.TileList.Count;i++)
                {
                    Tile t = env.TileList[i];

                    if(t.type == Tile.Type.Floor && PlayerSpawned && !TraderSpawned)
                    {
                        CharTrader Trader = new CharTrader(t);
                        env.Add(Trader);
                        TraderSpawned = true;
                    }

                    if(t.type == Tile.Type.Floor && !PlayerSpawned)
                    {
                        Player = new CharPlayer(t);
                        PlayerSpawned = true;
                        i += 4;
                    }

                    if (TraderSpawned && PlayerSpawned) break;
                }
            }
            return Player;
        }
    }
}
