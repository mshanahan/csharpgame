using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{

    class ItemWeapon : Item
    {
        public int Dice { get; set; }
        public int Sides { get; set; }
        public ItemWeapon(int price, int dice, int sides) : base(price)
        {
            this.Dice = dice;
            this.Sides = sides;
        }

        public Tuple<int,int> GetDamage()
        {
            int min = this.Dice;
            int max = this.Dice * this.Sides;
            return new Tuple<int, int>(min, max);
        }

        public int RollDamage()
        {
            int damage = 0;
            Environment env = Environment.Current();
            for(int i=0;i<Dice;i++)
            {
                damage += env.Random.Next(1, this.Sides + 1);
            }
            return damage;
        }
    }
}
