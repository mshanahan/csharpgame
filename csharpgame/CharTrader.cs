using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    public class CharTrader : Character
    {
        public static Texture2D TraderImage { get; set; }
        public static Texture2D TraderDeathImage { get; set; }
        public static int AttackCount { get; set; } = 1;
        public static int DamageCount { get; set; } = 1;
        public static int DefenseCount { get; set; } = 1;
        public static int HitpointCount { get; set; } = 1;
        public static List<CharTrader> TraderList { get; set; } = new List<CharTrader>();

        public CharTrader(Tile t) : base(t)
        {
            Environment env = Environment.Current();
            this.Name = "Trader";
            this.CurrentHitpoints = 1024;
            this.MaxHitpoints = CurrentHitpoints;
            this.Armor = 500;
            this.Attack = 0;
            this.Damage = 1;
            this.texture = TraderImage;
            this.DeathTexture = TraderDeathImage;
            this.behavior = Behavior.None;
            TraderList.Add(this);
        }
    }
}
