//==========================================================
// Student Number : S10257702
// Student Name : Jia Xiang
// Partner Name : Asyiq Nuruddin
//==========================================================
using System;
using System.Collections.Generic;
using PRG2_Assignment_Order;
using PRG2_Assignment_PointCard;
// Make sure to include the correct namespace for the Order class

namespace PRG2_Assignment_Customer
{
    internal class customer
    {
        public string Name { get; set; }
        public int MemberId { get; set; }
        public DateTime Dob { get; set; }
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; }
        public PointCard Rewards { get; set; }

        public customer()
        {
            CurrentOrder = new Order();
            OrderHistory = new List<Order>();
        }

        public customer(string name, int memberId, DateTime birth)
        {
            Name = name;
            MemberId = memberId;
            Dob = birth;
            CurrentOrder = new Order();
            OrderHistory = new List<Order>();
        }

        public void MakeOrder()
        {
            // Increment the order ID to make it unique
            int newOrderId = OrderHistory.Count + 1;

            // Create a new order with a unique ID and the current timestamp
            CurrentOrder = new Order(newOrderId, DateTime.Now);

            // Move the current order to order history
            if (CurrentOrder != null)
            {
                OrderHistory.Add(CurrentOrder);
            }
            
        }

        public bool IsBirthday()
        {
            
            
            if ( Dob == DateTime.Now) { 
                return true;
            
            }
            else { return false; }
            
        }

        public override string ToString()
        {
            return $"{Name} {MemberId} {Dob}";
        }
    }
}