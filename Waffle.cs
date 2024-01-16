//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
// Partner Name : Jia Xiang
//==========================================================
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
            double price = 0;
            if (WaffleFlavour != "Original")
            {
                price += 3;
            }
            if (Scoops == 1)
            {
                price += 7;
            }
            else if (Scoops == 2)
            {
                price += 8.50;
            }
            else if (Scoops == 3)
            {
                price += 9.50;
            }
            foreach (var f in Flavours)
            {
                if (f.Premium == true)
                {
                    price += 2;
                }
            }
            foreach (var t in Toppings)
            {
                price += 1;
            }
            return price;
        }
        public override string ToString()
        {
            string flavstring = $"\n---------------------------------\n{"Flavours",-21} {"Qty",-6}{"Price",-4}";
            string topstring = $"\n---------------------------------\n{"Toppings",-28}{"Price",-4}";
            foreach (var f in Flavours)
            {
                if (f.Premium == true)
                {
                    flavstring += $"\n{f.Type,-22} {f.Quantity,-5}{"$2.00",5}";
                }
                else
                {
                    flavstring += $"\n{f.Type,-22} {f.Quantity,-5}{"$0.00",5}";
                }
            }
            foreach (var t in Toppings)
            {
                topstring += $"\n{t.Type,-18}{"",-10}{"$1.00",-5}";
            }
            double price = 0;
            if (Scoops == 1)
            {
                price += 7;
            }
            else if (Scoops == 2)
            {
                price += 8.50;
            }
            else if (Scoops == 3)
            {
                price += 9.50;
            }
            if (WaffleFlavour != "Plain")
            {
                return $"\n{"Ice Cream Type: "+Option,-28}{"Price",-5}\n{$"Waffle Flavour: {WaffleFlavour}",-28}{"$3.00",-5}\n---------------------------------\n{"Scoops: " + Scoops,-28}{$"${price:0.00}",-5}{flavstring}{topstring}\n------------------------------\n{"Total",-28}${CalculatePrice():0.00}";
            }
            else
            {
                return $"\n{"Ice Cream Type: " + Option,-28}{"Price",-5}\n{$"Waffle Flavour: {WaffleFlavour}",-28}{"$0.00",-5}\n---------------------------------\n{"Scoops: " + Scoops,-28}{$"${price:0.00}",-5}{flavstring}{topstring}\n------------------------------\n{"Total",-28}${CalculatePrice():0.00}";
            }
        }
    }
}
