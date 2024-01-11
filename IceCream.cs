//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
// Partner Name : Jia Xiang
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRG2_Assignment_Topping;
using PRG2_Assignment_Flavour;

namespace PRG2_Assignment_IceCream
{
    abstract class IceCream
    {
        private string option;
        public string Option 
        { 
            get { return option; } 
            set { option = value; }
        }
        private int scoops;
        public int Scoops
        {
            get { return scoops; }
            set { scoops = value; }
        }
        private List<Flavour> flavours;
        public List<Flavour> Flavours
        {
            get { return flavours; }
            set { flavours = value; }
        }
        private List<Topping> toppings;
        public List<Topping> Toppings
        {
            get { return toppings; }
            set { toppings = value; }
        }
        public IceCream() { }
        public IceCream(string option,int scoops, List<Flavour> flavours, List<Topping> toppings) 
        {
            Option = option;
            Scoops = scoops;
            Flavours = flavours;
            Toppings = toppings;
        }
        public abstract double CalculatePrice();
        public override string ToString()
        {
            string flavstring = $"\nFlavours {"Qty",-10}\n---------------";
            string topstring = $"\nToppings\n---------------";
            foreach (var f in Flavours)
            {
                flavstring += $"\n{f.Type,-10} {f.Quantity,-10}";
            }
            foreach (var t in Toppings)
            {
                topstring += $"\n{t.Type,-10}";
            }
            return $"Ice Cream Type: {Option}\nScoop Count: {Scoops} {flavstring}{topstring}";
        }

    }
}
