using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class CharKobold : Character
    {
        public static Texture2D Image { get; set; }
        public static Texture2D DeathImage { get; set; }

        public CharKobold(Tile t) : base (t)
        {
            Environment env = Environment.Current();
            this.Name = "Kobold";
            this.CurrentHitpoints = env.Random.Next(1, 5);
            this.MaxHitpoints = CurrentHitpoints;
            this.Armor = 10;
            this.Attack = 0;
            this.Damage = 1;
            this.texture = Image;
            this.DeathTexture = DeathImage;
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
            int count = env.Random.Next(1, 7);
            for (int i = 0; i < count; i++)
            {
                env.Add(new CharKobold(t));
            }
        }
    }
}
