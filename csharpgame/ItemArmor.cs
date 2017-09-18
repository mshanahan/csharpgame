using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    class ItemArmor : Item
    {
        public int Armor { get; set; }
        
        public ItemArmor(int price, int armor) : base(price)
        {
            this.Armor = armor;
        }
    }
}
