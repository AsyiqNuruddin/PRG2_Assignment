using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Flavour
    {
        private string type;
        public string Type 
        {
            get { return type; } 
            set { type = value; }
        }
        private bool premium;
        public bool Premium
        {
            get { return premium; }
            set { premium = value; }
        }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public Flavour() { }
        public Flavour(string type,bool premium, int quantity) 
        { 
            Type = type;
            Premium = premium;
            Quantity = quantity;
        }
        public override string ToString()
        {
            return $"Type: {Type} Premuim: {Premium} Quantity: {Quantity}";
        }
    }
}
