using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Item
    {
        private string slot = "";
        private string description = "";
        private int priceInCents = 0;
        private int inventory = 0;

        public string Slot { get { return slot; } set { slot = value; } }
        public string Description { get { return description; } set { description = value; } }
        public int PriceInCents { get { return priceInCents; } set { priceInCents = value; } }
        public int Inventory { get { return inventory; } set { inventory = value; } }

        public int TotalItemSold { get; set; }
    }
}
