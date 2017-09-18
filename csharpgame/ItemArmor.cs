using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    public class ItemArmor : Item
    {
        public int Armor { get; set; }
        
        public ItemArmor(string name, int price, int armor) : base(name, price)
        {
            this.Armor = armor;
        }
    }
}
