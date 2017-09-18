using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class CharGoblin : Character
    {
        public static Texture2D GoblinImage { get; set; }
        public static Texture2D GoblinDeathImage { get; set; }

        public CharGoblin(Tile t) : base (t)
        {
            Environment env = Environment.Current();
            this.Name = "Goblin";
            this.CurrentHitpoints = env.Random.Next(1, 7);
            this.MaxHitpoints = CurrentHitpoints;
            this.Armor = 12;
            this.Attack = 0;
            this.Damage = 1;
            this.texture = GoblinImage;
            this.DeathTexture = GoblinDeathImage;
            this.behavior = Behavior.Wandering;
        }

        public override int GiveGold()
        {
            Environment env = Environment.Current();
            return env.Random.Next(1, 7);
        }

        public static void Spawn(Tile t)
        {
            Environment env = Environment.Current();
            int count = env.Random.Next(1, 5);
            for(int i=0;i<count;i++)
            {
                env.Add(new CharGoblin(t));
            }
        }
    }
}
