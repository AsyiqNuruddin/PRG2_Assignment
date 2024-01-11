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

namespace PRG2_Assignment_PointCard
{
    
        internal class PointCard
        {
            public int points { get; set; }
            public int punchCard { get; set; }
            public string tier { get; set; }
            public PointCard() { }

            public PointCard(int Points, int PunchCard)
            {

                points = Points;
                punchCard = PunchCard;
            }
            public void AddPoints(int poi)
            {


            }
            public void RedeemPoints(int poi) { }
            public void Punch() { }

            public override string ToString()
            {

                return $"{points}   {tier}   {punchCard}";
            }
        }
}


