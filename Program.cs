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
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using static System.Formats.Asn1.AsnWriter;
using System.Runtime.Intrinsics.X86;


//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
Dictionary<int, customer> DictCustomer = new Dictionary<int, customer>();
Dictionary<int, Flavour> DictFlavour = new Dictionary<int, Flavour>();
Dictionary<int, Topping> DictTopping = new Dictionary<int, Topping>();

InitCustomer("customers.csv");
InitFlavour(DictFlavour);
InitToppings(DictTopping);
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
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
void InitCustomer(string txtfile)
{
    DictCustomer = new Dictionary<int, customer>();
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
//==========================================================
void InitFlavour(Dictionary<int, Flavour> df)
{
    df.Add(1,new Flavour("Vanilla", false, 1));
    df.Add(2,new Flavour("Chocolate", false, 1));
    df.Add(3,new Flavour("Strawberry", false, 1));
    df.Add(4,new Flavour("Durian", true, 1));
    df.Add(5,new Flavour("Ube", true, 1));
    df.Add(6,new Flavour("Sea Salt", true, 1));
}
void InitToppings(Dictionary<int, Topping> dt)
{
    dt.Add(1,new Topping("Sprinkles"));
    dt.Add(2,new Topping("Mochi"));
    dt.Add(3,new Topping("Sago"));
    dt.Add(4,new Topping("Oreos"));
}
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
void DisplayFlavours(Dictionary<int, Flavour> df)
{
    int count = 1;
    foreach (var flavour in df.Values)
    {
        Console.WriteLine($"[{count}]: {flavour.Type,-10}");
        count++;
    }
}
void DisplayToppings(Dictionary<int, Topping> dt)
{
    int count = 1;
    foreach (Topping topping in dt.Values)
    {
        Console.WriteLine($"[{count}]: {topping.Type,-10}");
        count++;
    }
}


//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
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
    foreach (var kvp in DictCustomer)
    {
        Console.WriteLine($"Name: {kvp.Value.name,-10} Member ID:{kvp.Value.memberid,-10} DateofBirth: {kvp.Value.dob,-10:dd/MM/yyyy}");
        Console.WriteLine("orders");
        customer customer = kvp.Value;
        if(customer != null && customer.currentOrder != null && customer.currentOrder.IceCreamlist != null) {
            foreach (IceCream or in customer.currentOrder.IceCreamlist) {
                Console.WriteLine(or);
    

            }
        }
        else { Console.WriteLine(); }
            
               


           
                
                
    }

}


//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
void Option3() 
{
    Console.WriteLine("Registration of a new customer");
    Console.Write("Enter their Name: ");
    string nameInp = Console.ReadLine();
    Console.Write("Enter their ID Number: ");
    int idInp = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter their Date Of Birth in DD/MM/YYYY: ");
    DateTime dob = Convert.ToDateTime(Console.ReadLine());
    customer newCustomer = new customer(nameInp,idInp,dob);
    Console.WriteLine("Their registration customer details");
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
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
static customer? Search(Dictionary<int,customer> sDict, int userInp)
{
    foreach (var v in sDict)
    {
        if (v.Value.memberid == userInp)
        {
            return v.Value;
        }
    }
    return null;
}
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
(int, List<Flavour>, List<Topping>) IceCreamAdd(Dictionary<int,Flavour> df, Dictionary<int, Topping> dt)
{
    Console.Write("Enter number of scoops: ");
    int scoops = Convert.ToInt32(Console.ReadLine());
    List<Flavour> flavList = new List<Flavour>();
    for(int i = 1; i < scoops + 1; i++)
    {
        DisplayFlavours(DictFlavour);
        Console.Write($"Choose the flavour of scoop [{i}]: ");
        int flvIndex = Convert.ToInt32(Console.ReadLine());
        if (df.ContainsKey(flvIndex))
        {
            if (flavList.Contains(df[flvIndex]))
            {
                foreach (var v in flavList)
                {
                    if(v == df[flvIndex])
                    {
                        v.Quantity = v.Quantity + 1;
                    }
                }
            }
            else
            {
                flavList.Add(df[flvIndex]);
            }
        }
    }
    Console.Write("Enter number of toppings: ");
    int topCount = Convert.ToInt32(Console.ReadLine());
    List<Topping> topList = new List<Topping>();
    for (int i = 0;i < topCount;i++)
    {
        DisplayToppings(DictTopping);
        Console.Write($"Choose the [{i}] topping : ");
        int topIndex = Convert.ToInt32(Console.ReadLine());
        if (dt.ContainsKey(topIndex))
        {
            topList.Add(dt[topIndex]);
        }
    }
    return (scoops, flavList, topList);
}
string? WaffleChoice(string wafInp)
{
    wafInp = wafInp.ToLower();
    if(wafInp != null)
    {
        if(wafInp == "red velvet")
        {
            return wafInp;
        }else if(wafInp == "charcoal")
        {
            return wafInp;
        }
        else if(wafInp == "pandan waffle")
        {
            return wafInp;
        }
        else
        {
            return null;
        }
    }
    else
    {
        return null;
    }
    
}
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
void Option4() 
{
    // Refresh customer obj data
    InitCustomer("customers.csv");
    Option1(DictCustomer);
    Console.Write("Select the customer: ");
    int idInp = Convert.ToInt32(Console.ReadLine());
    customer? result = Search(DictCustomer, idInp);
    if (result != null)
    {
        Console.WriteLine("Found Customer ");
        Order cusOrder = new Order();
        Console.Write("Enter their ice cream order type (Cup, Cone or Waffle): ");
        string choiceInp = Console.ReadLine();
        choiceInp = choiceInp.ToLower();
        if(choiceInp == "cup")
        {
            Console.WriteLine("Chosen Cup");
            (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour,DictTopping);
            IceCream newice = new Cup("Cup", cat.Item1, cat.Item2, cat.Item3);
        }
        else if (choiceInp == "cone")
        {
            Console.WriteLine($"Chosen Cone");
            Console.Write("Do you want your cone dipped?(Y/N)");
            string dipInp = Console.ReadLine();
            dipInp = dipInp.ToLower();
            if (dipInp == "y")
            {
                (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                IceCream newice = new Cone("Cone", cat.Item1, cat.Item2, cat.Item3, true);
            }
            else if (dipInp == "n")
            {
                (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                IceCream newice = new Cone("Cone", cat.Item1, cat.Item2, cat.Item3, false);
            }
        }
        else if (choiceInp == "waffle")
        {
            Console.WriteLine("Chosen Waffle");
            Console.Write("Do you want your cone dipped?(Red velvet, charcoal, or pandan waffle)");
            string wafInp = Console.ReadLine();
            string waf = WaffleChoice(wafInp);
            if (waf == "red velvet")
            {
                (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                IceCream newice = new Waffle("Waffle", cat.Item1, cat.Item2, cat.Item3, wafInp);
            }
            else if (wafInp == "charcoal")
            {
                (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                IceCream newice = new Waffle("Waffle", cat.Item1, cat.Item2, cat.Item3, wafInp);
            }
            else if (wafInp == "pandan waffle")
            {
                (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                IceCream newice = new Waffle("Waffle", cat.Item1,cat.Item2, cat.Item3, wafInp);
            }
            
        }
    }
    else
    {
        Console.WriteLine("Customer not found");
    }

}
void Option5() {
    //ist the customers
    //prompt user to select a customer and retrieve the selected customer
    //retrieve all the order objects of the customer, past and current
    // for each order, display all the details of the order including datetime received, datetime
    //fulfilled(if applicable) and all ice cream details associated with the order

}
void Option6() {
    InitCustomer("customers.csv");
    Option1(DictCustomer);
    Console.Write("Select the customer: ");
    int idInp = Convert.ToInt32(Console.ReadLine());
    customer? result = Search(DictCustomer, idInp);

    if (result != null) { 
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
                result.currentOrder.AddIceCream(newice);

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
                    result.currentOrder.AddIceCream(newice);

                }
                else if (dipped == "no")
                {

                    IceCream newice = new Cone(newopt, newscp, flavlist, toplist, false);
                    result.currentOrder.AddIceCream(newice);

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
                        IceCream newice = new Waffle(newopt, newscp, flavlist, toplist, waffleFlavor);
                        result.currentOrder.AddIceCream(newice);

                    }






                }

            }
            else {
                Console.WriteLine();

            }
        }
        



        else if (choice == 3) { }
    }
}

