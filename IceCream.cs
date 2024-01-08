using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRG2_Assignment_Topping;
using PRG2_Assignment_Flavour;
namespace PRG2_Assignment
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
        public double CalculatePrice()
        {
            double price = 0;

            return price;
        }
        public override string ToString()
        {
            return $"{Option} {Scoops} {Flavours} {Toppings}";
        }

    }
}
