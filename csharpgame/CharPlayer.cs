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
        public static int MaxTicks { get; set; }
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
            this.Armor = 100;
            this.Attack = 0;
            this.Damage = 1;
            this.texture = PlayerImage;
            this.DeathTexture = PlayerDeathImage;
            this.Locked = false;
            this.Gold = 0;
            this.TorchCount = 6;
            this.TorchTicks = 0;
        }

        public static CharPlayer GetPlayer()
        {
            if(Player == null)
            {
                Environment env = Environment.Current();
                Player = new CharPlayer(env.TileList[0]);
            }
            return Player;
        }
    }
}
