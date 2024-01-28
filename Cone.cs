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
            double price = 0;
            if (Dipped)
            {
                price += 2;
            }
            if (Scoops == 1)
            {
                price += 4;
            }
            else if (Scoops == 2)
            {
                price += 5.50;
            }
            else if (Scoops == 3)
            {
                price += 6.50;
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
                price += 4;
            }
            else if (Scoops == 2)
            {
                price += 5.50;
            }
            else if (Scoops == 3)
            {
                price += 6.50;
            }
            if (dipped)
            {
                return $"\n{"Ice Cream Type: " + Option,-28}{"Price",-5}\n{"Dipped with Chocolate Cone",-28}{"$2.00",-5}\n---------------------------------\n{"Scoops: " + Scoops,-28}{$"${price:0.00}",-5}{flavstring}{topstring}\n---------------------------------\n{"Total",-28}${CalculatePrice():0.00}";
            }
            else
            {
                return $"\n{"Ice Cream Type: " + Option,-28}{"Price",-5}\n{"Plain Cone",-28}{"$0.00",-5}\n---------------------------------\n{"Scoops: "+Scoops,-28}{$"${price:0.00}",-5}{flavstring}{topstring}\n---------------------------------\n{"Total",-28}${CalculatePrice():0.00}";
            }
            
        }
    }
}
