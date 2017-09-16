using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class Goblin : Character
    {
        public static Texture2D GoblinImage { get; set; }
        public static Texture2D GoblinDeathImage { get; set; }

        public Goblin(Tile t) : base (t)
        {
            Environment env = Environment.Current();
            this.Name = "Goblin";
            this.CurrentHitpoints = env.Random.Next(1, 5);
            this.MaxHitpoints = CurrentHitpoints;
            this.Armor = 12;
            this.Attack = 0;
            this.Damage = 1;
            this.texture = GoblinImage;
            this.DeathTexture = GoblinDeathImage;
        }
    }
}
