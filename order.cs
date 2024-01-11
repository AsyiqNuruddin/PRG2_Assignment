//==========================================================
// Student Number : S10257702
// Student Name : Jia Xiang
// Partner Name : Asyiq Nuruddin
//==========================================================
using System;
using PRG2_Assignment_IceCream;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace PRG2_Assignment_Order
{
    internal class Order
    {
        
        public int id {  get; set; }
        public DateTime timeRecieved { get; set; }
        public DateTime? timeFulfilled { get; set; }
        public List<IceCream> IceCreamlist
        { get; set; }
        public Order() 
        {
            IceCreamlist = new List<IceCream>();
        }
        public Order(int Id, DateTime tR) {
            id = Id;
            timeRecieved = tR;
            IceCreamlist = new List<IceCream>();

        }
        public void Modifyicecream(int id) {
            IceCream modice = IceCreamlist[id-1];
            
            
            


        }
        public void AddIceCream(IceCream ice) {
            IceCreamlist.Add(ice);
        
        }
        public void DeleteIceCream(int id) {
            IceCreamlist.Remove(IceCreamlist[id-1]);
        
        }
        public static double CalcualteTotal() {

            double total = 0;
            return total;

        }
        public override string ToString()
        {
            return $"{id}{Convert.ToString(timeRecieved)}";
        }
    }

}
