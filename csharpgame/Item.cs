using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    public class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public Item(string Name, int Price)
        {
            this.Name = Name;
            this.Price = Price;
        }
    }
}
