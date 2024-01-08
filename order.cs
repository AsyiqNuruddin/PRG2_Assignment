using System;
using PRG2_Assignment_IceCream;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Order
    {
        public int id {  get; set; }
        public DateTime timeRecieved { get; set; }
        public DateTime? timeFulfilled { get; set; }
        public List<IceCream> IceCreamlist
        { get; set; }
        public Order() { }
        public Order(int Id, DateTime tR) {
            id = Id;
            timeRecieved = tR;

        }
        public void Modifyicecream(int id) { }
        public void AddIceCream(IceCream ice) { }
        public void DeleteIceCream(int id) { }
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
