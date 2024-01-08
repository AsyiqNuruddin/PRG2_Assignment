//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
// Partner Name : Jia Xiang
//==========================================================
using PRG2_Assignment;
using PRG2_Assignment_Flavour;
using PRG2_Assignment_IceCream;
using PRG2_Assignment_Topping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment_Cup
{
    internal class Cup : IceCream
    {
        public Cup():base() { }
        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) :base(option,scoops,flavours,toppings) 
        {

        }
        public override double CalculatePrice()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return $"{Option} {Scoops} {Flavours} {Toppings}";
        }
    }
}
