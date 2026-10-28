
using System;

namespace ED262C
{
    public class ShopItem : IComparable
    {
        public string Name;
        public int Price;

        public ShopItem(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public int CompareTo(object obj)
        {
            ShopItem other = (ShopItem)obj;
            return Price.CompareTo(other.Price);
        }

        public override string ToString()
        {
            return Name + " ($" + Price + ")";
        }
    }
}
