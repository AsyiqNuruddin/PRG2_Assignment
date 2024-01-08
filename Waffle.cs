using PRG2_Assignment_Flavour;
using PRG2_Assignment_IceCream;
using PRG2_Assignment_Topping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Waffle : IceCream
    {
        private string waffleFlavour;
        public string WaffleFlavour
        {
            get { return waffleFlavour; }
            set { waffleFlavour = value; }
        }
        public Waffle() { }
        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour) :base(option, scoops, flavours, toppings)
        { 
            WaffleFlavour = waffleFlavour;
        }
        public override double CalculatePrice()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return $"{Option} {Scoops} {Flavours} {Toppings} {WaffleFlavour}";
        }
    }
}
