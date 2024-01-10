//==========================================================
// Student Number : S10257702
// Student Name : Jia Xiang
// Partner Name : Asyiq Nuruddin
//==========================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class customer
    {
        public string name { get; set; }
        public int memberid { get; set; }
        public DateTime dob { get; set; }
        public Order currentOrder { get; set; }
        public List<Order> orderHistory { get; set; }
        public PointCard rewards { get; set; }
        public customer() { }
        public customer(string nam,int member,DateTime brith) 
        {

            name = nam;
            memberid = member;
            dob = brith;
            orderHistory = new List<Order>();
        
        
        }
        public static Order Makeorder() {
            Order order = new Order(,DateTime.Now);
        
        
        }
        public static bool isBirthday() { }
        public override string ToString()
        {
            return $"{name}{memberid}{dob}";
        }

    }
}
