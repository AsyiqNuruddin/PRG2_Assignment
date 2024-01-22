using PRG2_Assignment_Order;
using PRG2_Assignment_PointCard;
using System;
using System.Collections.Generic;

namespace PRG2_Assignment_Customer
{
    internal class Customer

    {
        private static int nextOrderId = 1;  // Static variable to keep track of the next available order ID

        public string Name { get; set; }
        public int MemberId { get; set; }
        public DateTime Dob { get; set; }
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; }
        public PointCard Rewards { get; set; }

        public Customer
()
        {
            CurrentOrder = new Order();
            OrderHistory = new List<Order>();
        }

        public Customer
(string name, int memberId, DateTime birth)
        {
            Name = name;
            MemberId = memberId;
            Dob = birth;
            CurrentOrder = new Order();
            OrderHistory = new List<Order>();
        }

        public void MakeOrder()
        {
            // Create a new order with a unique ID and the current timestamp
            CurrentOrder = new Order(nextOrderId, DateTime.Now);

            // Increment the next available order ID for the next order
            nextOrderId++;

            // Move the current order to order history
            if (CurrentOrder != null)
            {
                OrderHistory.Add(CurrentOrder);
            }
        }

        public bool IsBirthday()
        {
            // Check if the current date matches the customer's birthday
            return Dob.Month == DateTime.Now.Month && Dob.Day == DateTime.Now.Day;
        }

        public override string ToString()
        {
            return $"{Name} {MemberId} {Dob}";
        }
    }
}
