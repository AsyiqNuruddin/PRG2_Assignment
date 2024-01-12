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
            string flavstring = $"\n---------------\n{"Flavours",-10} {"Qty",-10}";
            string topstring = $"\n---------------\nToppings";
            int fcount = 1;
            foreach (var f in Flavours)
            {
                flavstring += $"\n[{fcount}]{f.Type,-10} {f.Quantity,-10}";
                fcount++;
            }
            int tcount = 1;
            foreach (var t in Toppings)
            {
                topstring += $"\n[{tcount}]{t.Type,-10}";
                tcount++;
            }
            return $"Ice Cream Type: {Option}\n---------------\nScoop Count: {Scoops} {flavstring}{topstring}";
        }
    }
}
