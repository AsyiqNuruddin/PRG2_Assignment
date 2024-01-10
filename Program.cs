using PRG2_Assignment;
using PRG2_Assignment_Order;
using PRG2_Assignment_PointCard;
using PRG2_Assignment_Customer;
using PRG2_Assignment_Cone;
using PRG2_Assignment_Cup;
using PRG2_Assignment_Flavour;
using PRG2_Assignment_IceCream;
using PRG2_Assignment_Topping;
using System;


//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
// Partner Name : Jia Xiang
//==========================================================
Dictionary<int, customer> DictCustomer = new Dictionary<int, customer>();


InitCustomer("customers.csv");
// Loop of Options
while (true) 
{
    Console.Write("Please enter your option: ");
    string usrInp = Console.ReadLine();
    if (usrInp == "1")
    {
        Option1(DictCustomer);
    }else if (usrInp == "2")
    {
        Option2();
    }else if (usrInp == "3")
    {
        Option3();
    }else if (usrInp == "4")
    {
        Option4();
    }else if (usrInp == "5")
    {
        Option5();
    }else if (usrInp == "6")
    {
        Option6();
    }else if(usrInp == "-1")
    {
        Console.WriteLine("Thanks for using the application!");
        break;
    }
}

void InitCustomer(string txtfile)
{
    using (StreamReader sr = new StreamReader(txtfile))
    {
        List<string> rowList = new List<string>();
        List<string> headers = new List<string>();

        string? s = sr.ReadLine();
        if (s != null)
        {
            headers = s.Split(",").ToList();
        }
        while ((s = sr.ReadLine()) != null)
        {
            rowList = s.Split(',').ToList();

            DictCustomer.Add(Convert.ToInt32(rowList[1]), new customer(rowList[0], Convert.ToInt32(rowList[1]), Convert.ToDateTime(rowList[2])) );
        }
    }
}
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
// Partner Name : Jia Xiang
//==========================================================

void Option1(Dictionary<int, customer> DictCustomer) 
{
    foreach(var kvp in DictCustomer)
    {
        Console.WriteLine($"Name: {kvp.Value.name,-10} Member ID:{kvp.Value.memberid,-10} DateofBirth: {kvp.Value.dob,-10:dd/MM/yyyy}");
    }
}
void Option2() 
{ 

}


//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
void Option3() 
{
    Console.WriteLine("Registration of a new customer");
    Console.Write("Enter your Name: ");
    string nameInp = Console.ReadLine();
    Console.Write("Enter your ID Number: ");
    int idInp = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter your Date Of Birth in DD/MM/YYYY: ");
    DateTime dob = Convert.ToDateTime(Console.ReadLine());
    customer newCustomer = new customer(nameInp,idInp,dob);
    Console.WriteLine("Your registration customer details");
    Console.WriteLine($"Name: {newCustomer.name,-10} Member ID:{newCustomer.memberid,-10} DateofBirth: {newCustomer.dob,-10:dd/MM/yyyy}");
    PointCard newPC = new PointCard(0,0);
    newCustomer.rewards = newPC;

    using (StreamWriter sw = new StreamWriter("customers.csv", true))
    {
        string? row;
        row = string.Join(",", newCustomer.name, newCustomer.memberid, $"{newCustomer.dob:dd/MM/yyyy}");
        sw.WriteLine(row);
    }
    Console.WriteLine("Registration Successfull");

}
void Option4() { }
void Option5() {
    //ist the customers
    //prompt user to select a customer and retrieve the selected customer
    //retrieve all the order objects of the customer, past and current
    // for each order, display all the details of the order including datetime received, datetime
    //fulfilled(if applicable) and all ice cream details associated with the order

}
void Option6() {
    Console.WriteLine("Menu:\r\n1. Modify an existing ice cream in the order\r\n2. Add a new ice cream to the order\r\n3. Delete an existing ice cream from the order");
    Console.Write("Please enter the number corresponding to your choice: ");
    int choice = Convert.ToInt32(Console.ReadLine());
    if (choice == 1) { }
    else if (choice == 2)
    {
        List<Flavour> flavlist = new List<Flavour>();
        List<Topping> toplist = new List<Topping>();
        Console.WriteLine("Enter optionn : ");
        string newopt = Console.ReadLine();
        if (newopt == "cup")
        {

            Console.WriteLine("Enter number of scoops: ");


            int newscp = Convert.ToInt16(Console.ReadLine());
            for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
            {
                Console.WriteLine($"Enter flavour number {scoopIndex} : ");
                string newflav = Console.ReadLine();
                if (newflav.ToLower() == "durian" || newflav.ToLower() == "ube" || newflav.ToLower() == "sea slat")
                {
                    foreach (Flavour flav in flavlist)
                    {
                        if (flav.Type == newflav.ToLower())
                        {
                            flav.Quantity += 1;


                        }
                        else
                        {
                            Flavour flavour = new Flavour(newflav, true, 1);
                            flavlist.Add(flavour);

                        }

                    }



                }
                else
                {

                    foreach (Flavour flav in flavlist)
                    {
                        if (flav.Type == newflav.ToLower())
                        {
                            flav.Quantity += 1;


                        }
                        else
                        {
                            Flavour flavour = new Flavour(newflav, false, 1);
                            flavlist.Add(flavour);
                        }

                    }
                }


            }
            Console.WriteLine("Enter number of toppings: ");
            int newtop = Convert.ToInt16(Console.ReadLine());

            for (int topIndex = 1; topIndex <= newtop; topIndex++)
            {
                Console.WriteLine($"Enter topping number {1} : ");
                string top = Console.ReadLine();
                Topping tops = new Topping(top);
                toplist.Add(tops);



            }
            IceCream newice = new Cup(newopt, newscp, flavlist, toplist);
        }
        else if (newopt == "cone")
        {
            Console.WriteLine("Enter number of scoops: ");


            int newscp = Convert.ToInt16(Console.ReadLine());
            for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
            {
                Console.WriteLine($"Enter flavour number {scoopIndex} : ");
                string newflav = Console.ReadLine();
                if (newflav.ToLower() == "durian" || newflav.ToLower() == "ube" || newflav.ToLower() == "sea slat")
                {
                    foreach (Flavour flav in flavlist)
                    {
                        if (flav.Type == newflav.ToLower())
                        {
                            flav.Quantity += 1;


                        }
                        else
                        {
                            Flavour flavour = new Flavour(newflav, true, 1);
                            flavlist.Add(flavour);

                        }

                    }



                }
                else
                {

                    foreach (Flavour flav in flavlist)
                    {
                        if (flav.Type == newflav.ToLower())
                        {
                            flav.Quantity += 1;


                        }
                        else
                        {
                            Flavour flavour = new Flavour(newflav, false, 1);
                            flavlist.Add(flavour);
                        }

                    }
                }


            }
            Console.WriteLine("Enter number of toppings: ");
            int newtop = Convert.ToInt16(Console.ReadLine());

            for (int topIndex = 1; topIndex <= newtop; topIndex++)
            {
                Console.WriteLine($"Enter topping number {1} : ");
                string top = Console.ReadLine();
                Topping tops = new Topping(top);
                toplist.Add(tops);



            }
            Console.WriteLine("Do you want it dipped(yes or no) : ");
            string dipped = Console.ReadLine();
            if (dipped == "yes")
            {
                IceCream newice = new Cone(newopt, newscp, flavlist, toplist, true);

            }
            else if (dipped == "no")
            {

                IceCream newice = new Cone(newopt, newscp, flavlist, toplist, false);

            }




        }
        else if (newopt == "waffle")
        {
            {
                Console.WriteLine("Enter number of scoops: ");


                int newscp = Convert.ToInt16(Console.ReadLine());
                for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
                {
                    Console.WriteLine($"Enter flavour number {scoopIndex} : ");
                    string newflav = Console.ReadLine();
                    if (newflav.ToLower() == "durian" || newflav.ToLower() == "ube" || newflav.ToLower() == "sea slat")
                    {
                        foreach (Flavour flav in flavlist)
                        {
                            if (flav.Type == newflav.ToLower())
                            {
                                flav.Quantity += 1;


                            }
                            else
                            {
                                Flavour flavour = new Flavour(newflav, true, 1);
                                flavlist.Add(flavour);

                            }

                        }



                    }
                    else
                    {

                        foreach (Flavour flav in flavlist)
                        {
                            if (flav.Type == newflav.ToLower())
                            {
                                flav.Quantity += 1;


                            }
                            else
                            {
                                Flavour flavour = new Flavour(newflav, false, 1);
                                flavlist.Add(flavour);
                            }

                        }
                    }


                }
                Console.WriteLine("Enter number of toppings: ");
                int newtop = Convert.ToInt16(Console.ReadLine());

                for (int topIndex = 1; topIndex <= newtop; topIndex++)
                {
                    Console.WriteLine($"Enter topping number {1} : ");
                    string top = Console.ReadLine();
                    Topping tops = new Topping(top);
                    toplist.Add(tops);



                }
                Console.WriteLine("Enter wallfle flavour: ");
                string waffleFlavor = Console.ReadLine();
                if (waffleFlavor == "red velvet" || waffleFlavor == "charcoal" || waffleFlavor == "pandan")
                {
                    IceCream newice = new Waffle(newopt, newscp, flavlist, toplist,waffleFlavor );

                }
                






            }
        }



        else if (choice == 3) { }
    }
}

