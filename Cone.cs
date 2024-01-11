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

namespace PRG2_Assignment_Cone
{
    internal class Cone : IceCream
    {
        private bool dipped;
        public bool Dipped
        {
            get { return dipped; }
            set { dipped = value; }
        }
        public Cone():base() { }
        public Cone(string option, int scoops, List<Flavour> flavours, List<Topping> toppings,bool dipped):base(option, scoops, flavours, toppings)
        {
            Dipped = dipped;
        }
        public override double CalculatePrice()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            string flavstring = $"\n---------------\n{"Flavours",-10} {"Qty",-10}";
            string topstring = $"\n---------------\nToppings";
            foreach (var f in Flavours)
            {
                flavstring += $"\n{f.Type,-10} {f.Quantity,-10}";
            }
            foreach (var t in Toppings)
            {
                topstring += $"\n{t.Type,-10}";
            }
            if (dipped)
            {
                return $"Ice Cream Type: {Option}\n{"Dipped with Chocolate Cone",-10}\n---------------\nScoop Count: {Scoops} {flavstring}{topstring}";
            }
            else
            {
                return $"\nIce Cream Type: {Option}\n{"Plain Cone",-10}\n---------------\nScoop Count: {Scoops} {flavstring}{topstring}";
            }
            
        }
    }
}
