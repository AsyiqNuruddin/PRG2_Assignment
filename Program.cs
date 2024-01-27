using PRG2_Assignment;
using PRG2_Assignment_Order;
using PRG2_Assignment_PointCard;
using PRG2_Assignment_Customer;
using PRG2_Assignment_Cone;
using PRG2_Assignment_Cup;
using PRG2_Assignment_Flavour;
using PRG2_Assignment_IceCream;
using PRG2_Assignment_Topping;
using System.Globalization;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using static System.Formats.Asn1.AsnWriter;
using System.Runtime.Intrinsics.X86;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Linq;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Linq.Expressions;


//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
Dictionary<int, Customer>DictCustomer = new Dictionary<int, Customer>();
Queue<Order> GoldQueueOrder = new Queue<Order>();
Queue<Order> RegularQueueOrder = new Queue<Order>();
Dictionary<int, Flavour> DictFlavour = new Dictionary<int, Flavour>();
Dictionary<int, Topping> DictTopping = new Dictionary<int, Topping>();

try
{
    InitCustomer("customers.csv");
    InitFlavours("flavours.csv", DictFlavour);
    InitToppings("toppings.csv", DictTopping);
    InitOrders("orders.csv");
}
catch (IOException )
{
    Console.WriteLine("An error occurred while processing the files.");
}
catch (Exception )
{
    Console.WriteLine("An generic error ocurred from file reading.");
}

// Loop of Options

do{
    Intialdisplay();

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
    }else if (usrInp == "7")
    {
        // Option7();
    }else if (usrInp == "8")
    {
        // Option8();
    }
    else if(usrInp == "0")
    {
        Console.WriteLine("Thanks for using the application!");
        break;
    }
    else
    {
        Console.WriteLine("Please enter a appriopriate input [1 - 7] Option or [-1] to Exit");
    }
}
while (true);

void Intialdisplay() {
    Console.WriteLine("-------i.c treats icecream shop-------");
    Console.WriteLine("[1] List all customers");
    Console.WriteLine("[2] List all current orders");
    Console.WriteLine("[3] Register new customer");
    Console.WriteLine("[4] Create customer order");
    Console.WriteLine("[5] Display order detail of customner");
    Console.WriteLine("[6] Modify order detail");
    Console.WriteLine("[7] Check Out");
    Console.WriteLine("[8] Display cahrged amount break down"); 
    Console.WriteLine("[0] Exit");

}
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
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
            Customer newCustomer = new Customer (rowList[0], Convert.ToInt32(rowList[1]), Convert.ToDateTime(rowList[2]));
            newCustomer.Rewards = new PointCard(Convert.ToInt32(rowList[4]), Convert.ToInt32(rowList[5]));
            newCustomer.Rewards.tier = rowList[3];
            DictCustomer.Add(Convert.ToInt32(rowList[1]), newCustomer);
        }
    }
}
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
void InitFlavours(string txtfile, Dictionary<int, Flavour> df)
{
    using (StreamReader sr = new StreamReader(txtfile))
    {
        List<string> headers = new List<string>();
        List<string> rowList = new List<string>();
        int count = 1;

        string? s = sr.ReadLine();
        if (s != null)
        {
            headers = s.Split(",").ToList();
        }
        while ((s = sr.ReadLine()) != null)
        {
            rowList = s.Split(',').ToList();

            Flavour flav;
            if(Convert.ToInt32(rowList[1]) > 0)
            {
                flav = new Flavour(rowList[0], true, 1);
            }
            else
            {
                flav = new Flavour(rowList[0], false, 1);
            }
            df.Add(count, flav);
            count ++;
        }
    }
}
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
void InitToppings(string txtfile, Dictionary<int, Topping> dt)
{
    using (StreamReader sr = new StreamReader(txtfile))
    {
        List<string> headers = new List<string>();
        List<string> rowList = new List<string>();
        int count = 1;
        string? s = sr.ReadLine();
        if (s != null)
        {
            headers = s.Split(",").ToList();
        }
        while ((s = sr.ReadLine()) != null)
        {
            rowList = s.Split(',').ToList();

            dt.Add(count, new Topping(rowList[0]));
            count++;
        }
    }
}
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
void InitOrders(string txtfile)
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

            Order order = new Order(Convert.ToInt32(rowList[0]), Convert.ToDateTime(rowList[2]));
            order.timeFulfilled = Convert.ToDateTime(rowList[3]);
            // 4Option,5Scoops,6Dipped,7WaffleFlavour,8Flavour1,9Flavour2,10Flavour3,11Topping1,12Topping2,13Topping3,14Topping4
            IceCream newIC = IceCreamRead(rowList[4], Convert.ToInt32(rowList[5]), rowList[6], rowList[7], rowList[8], rowList[9], rowList[10], rowList[11], rowList[12], rowList[13], rowList[14]);
            foreach(var c in DictCustomer.Values)
            {
                if (c.OrderHistory != null)
                {
                    if (c.MemberId == Convert.ToInt32(rowList[1]))
                    {
                        if(c.OrderHistory.Count == 0)
                        {
                            order.AddIceCream(newIC);
                            c.OrderHistory.Add(order);
                        }
                        else
                        {
                            bool check = false;
                            foreach(var o in c.OrderHistory)
                            {
                                if(o.id == Convert.ToInt32(rowList[0]))
                                {
                                    o.AddIceCream(newIC);
                                    check = true;
                                    break;
                                }
                            }
                            if (!check)
                            {
                                order.AddIceCream(newIC);
                                c.OrderHistory.Add(order);
                            }
                        }


                    }
                }
            }
        }
    }
}
// 0Id,1MemberId,2TimeReceived,3TimeFulfilled,4Option,5Scoops,6Dipped,7WaffleFlavour,8Flavour1,9Flavour2,10Flavour3,11Topping1,12Topping2,13Topping3,14Topping4
IceCream IceCreamRead(string Option, int Scoops, string Dipped, string? WaffleFlavour, string? Flavour1, string? Flavour2, string? Flavour3, string? Topping1, string? Topping2, string? Topping3, string? Topping4)
{
    IceCream? newIC = new Cup();
    List<Flavour> flavList = new List<Flavour>();
    List<Topping> topList = new List<Topping>();
    if (Option == "Cup")
    {
        foreach (var v in DictFlavour)
        {
            if (v.Value.Type == Flavour1)
            {
                flavList.Add(v.Value);
            }
            else if (v.Value.Type == Flavour2)
            {
                flavList.Add(v.Value);
            }
            else if (v.Value.Type == Flavour3)
            {
                flavList.Add(v.Value);
            }

        }
        foreach (var v in DictTopping)
        {
            if (v.Value.Type == Topping1)
            {
                topList.Add(v.Value);
            }else if (v.Value.Type == Topping2)
            {
                topList.Add(v.Value);
            }else if(v.Value.Type == Topping3)
            {
                topList.Add(v.Value);
            }else if( v.Value.Type == Topping4)
            {
                topList.Add(v.Value);
            }
        }
        newIC = new Cup(Option,Scoops,flavList,topList);
    }
    else if (Option == "Cone")
    {
        foreach (var v in DictFlavour)
        {
            if (v.Value.Type == Flavour1)
            {
                flavList.Add(v.Value);
            }
            else if (v.Value.Type == Flavour2)
            {
                flavList.Add(v.Value);
            }
            else if (v.Value.Type == Flavour3)
            {
                flavList.Add(v.Value);
            }

        }
        foreach (var v in DictTopping)
        {
            if (v.Value.Type == Topping1)
            {
                topList.Add(v.Value);
            }
            else if (v.Value.Type == Topping2)
            {
                topList.Add(v.Value);
            }
            else if (v.Value.Type == Topping3)
            {
                topList.Add(v.Value);
            }
            else if (v.Value.Type == Topping4)
            {
                topList.Add(v.Value);
            }
        }
        if(Dipped == "TRUE")
        {
            newIC = new Cone(Option, Scoops, flavList, topList, true);
        }
        else if(Dipped == "FALSE")
        {
            newIC = new Cone(Option, Scoops, flavList, topList, false);
        }
        
    }
    else if (Option == "Waffle")
    {
        foreach (var v in DictFlavour)
        {
            if (v.Value.Type == Flavour1)
            {
                flavList.Add(v.Value);
            }
            else if (v.Value.Type == Flavour2)
            {
                flavList.Add(v.Value);
            }
            else if (v.Value.Type == Flavour3)
            {
                flavList.Add(v.Value);
            }

        }
        foreach (var v in DictTopping)
        {
            if (v.Value.Type == Topping1)
            {
                topList.Add(v.Value);
            }
            else if (v.Value.Type == Topping2)
            {
                topList.Add(v.Value);
            }
            else if (v.Value.Type == Topping3)
            {
                topList.Add(v.Value);
            }
            else if (v.Value.Type == Topping4)
            {
                topList.Add(v.Value);
            }
        }
        newIC = new Waffle(Option, Scoops, flavList, topList, WaffleFlavour);
    }
    return newIC;
}
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
void DisplayFlavours(Dictionary<int, Flavour> df)
{
    foreach (var v in df)
    {
        Console.WriteLine($"[{v.Key}]: {v.Value.Type,-10}");
    }
}
void DisplayToppings(Dictionary<int, Topping> dt)
{
    foreach (var v in dt)
    {
        Console.WriteLine($"[{v.Key}]: {v.Value.Type,-10}");
    }
}


//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================

void Option1(Dictionary<int, Customer
> DictCustomer) 
{
    foreach(var kvp in DictCustomer)
    {
        Console.WriteLine($"Name: {kvp.Value.Name,-15} Member ID:{kvp.Value.MemberId,-10} DateofBirth: {kvp.Value.Dob,-10:dd/MM/yyyy} MemberShip Status: {kvp.Value.Rewards.tier,-10} Points: {kvp.Value.Rewards.points,-3} Punch Card: {kvp.Value.Rewards.punchCard}");
        Console.WriteLine(kvp.Value.OrderHistory.Count);
    }
}
void Option2() 
{
    foreach (var kvp in DictCustomer)
    {
        Console.WriteLine($"Name: {kvp.Value.Name,-15} Member ID:{kvp.Value.MemberId,-10} DateofBirth: {kvp.Value.Dob,-10:dd/MM/yyyy}");
        Console.WriteLine("orders");
        Customer customer = kvp.Value;
        if(customer != null && customer.CurrentOrder != null && customer.CurrentOrder.IceCreamlist != null) {
            foreach (IceCream or in customer.CurrentOrder.IceCreamlist) {
                Console.WriteLine(or);
    

            }
        }
        
            
               


           
                
                
    }

}


//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
void Option3()
{
    while (true)
    {
        try
        {
            Console.WriteLine("Registration of a new customer");
            Console.Write("Enter their Name: ");
            string nameInp = Console.ReadLine();
            Console.Write("Enter their ID Number: ");
            int idInp = Convert.ToInt32(Console.ReadLine());
            bool same = false;
            foreach (var kvp in DictCustomer)
            {
                if (kvp.Key == idInp)
                {
                    same = true;
                }
            }
            if (!same)
            {
                Console.Write("Enter their Date Of Birth in DD/MM/YYYY: ");
                DateTime dob = Convert.ToDateTime(Console.ReadLine());
                Customer
                newCustomer = new Customer(nameInp, idInp, dob);
                Console.WriteLine("Their registration customer details");
                Console.WriteLine($"Name: {newCustomer.Name,-10} Member ID:{newCustomer.MemberId,-10} DateofBirth: {newCustomer.Dob,-10:dd/MM/yyyy}");
                PointCard newPC = new PointCard(0, 0);
                newPC.tier = "Ordinary";
                newCustomer.Rewards = newPC;

                using (StreamWriter sw = new StreamWriter("customers.csv", true))
                {
                    string? row;
                    row = string.Join(",", newCustomer.Name, newCustomer.MemberId, $"{newCustomer.Dob:dd/MM/yyyy}", newCustomer.Rewards.tier, newCustomer.Rewards.points, newCustomer.Rewards.punchCard);
                    sw.WriteLine(row);
                }
                DictCustomer.Add(newCustomer.MemberId, newCustomer);
                Console.WriteLine("Registration Successfull");
                break;
            }
            else
            {
                Console.WriteLine("Member ID Number used\nPlease use another Member ID Number");
            }
            
            
            
            
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid date format. Please enter a valid date in the format DD/MM/YYYY.");
            // You might want to handle the error, ask the user to enter the date again, or take appropriate action.
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");


        }
    }
}
    //==========================================================
    // Student Number : S10262791
    // Student Name : Asyiq Nuruddin
    //==========================================================
    static Customer? Search(Dictionary<int, Customer
    > sDict, int userInp)
    {
        foreach (var v in sDict)
        {
            if (v.Value.MemberId == userInp)
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
    (int, List<Flavour>, List<Topping>) IceCreamAdd(Dictionary<int, Flavour> df, Dictionary<int, Topping> dt)
    {
        Console.Write("Enter number of scoops [1-3]: ");
        int scoops = Convert.ToInt32(Console.ReadLine());
        List<Flavour> flavList = new List<Flavour>();
        if (scoops <= 3 && scoops > 0)
        {
            for (int i = 1; i < scoops + 1; i++)
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
                            if (v == df[flvIndex])
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
        }
        else
        {
            Console.WriteLine("Invalid number of scoops | Only 1 to 3 scoops");
        }
        Console.Write("Enter number of toppings [1-4]: ");
        int topCount = Convert.ToInt32(Console.ReadLine());
        List<Topping> topList = new List<Topping>();
        if (topCount > 0 && topCount <= 4)
        {
            for (int i = 1; i < topCount + 1; i++)
            {
                DisplayToppings(DictTopping);
                Console.Write($"Choose the [{i}] topping : ");
                int topIndex = Convert.ToInt32(Console.ReadLine());
                if (dt.ContainsKey(topIndex))
                {
                    topList.Add(dt[topIndex]);
                }
            }
        }
        else
        {
            Console.WriteLine("No toppings added");
        }

        return (scoops, flavList, topList);
    }
    string? WaffleChoice(string wafInp)
    {
        wafInp = wafInp.ToLower();
        if (wafInp != null)
        {
            if (wafInp == "red velvet")
            {
                return "Red Velvet";
            }
            else if (wafInp == "charcoal")
            {
                return "Charcoal";
            }
            else if (wafInp == "pandan")
            {
                return "Pandan";
            }
            else if (wafInp == "original")
            {
                return "Original";
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
    
        try {
            // show Customer Details
            Option1(DictCustomer);
            IceCream newice = null;
            Console.Write("Select the customer: ");
            int idInp = Convert.ToInt32(Console.ReadLine());
            Customer? result = Search(DictCustomer, idInp);
            if (result != null)
            {
                Console.WriteLine("Found Customer ");
                Order newOrd = result.CurrentOrder;
                newOrd.id = result.OrderHistory.Count + 1;
                newOrd.timeRecieved = DateTime.Now;
                while (true)
                {
                    Console.Write("Enter their ice cream order type (Cup, Cone or Waffle): ");

                    string choiceInp = Console.ReadLine();
                    choiceInp = choiceInp.ToLower();
                    if (choiceInp == "cup")
                    {
                        Console.WriteLine("Chosen Cup");
                        (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                        newice = new Cup("Cup", cat.Item1, cat.Item2, cat.Item3);
                    }
                    else if (choiceInp == "cone")
                    {
                        Console.WriteLine($"Chosen Cone");
                        Console.Write("Do you want your cone dipped?(Y/N): ");
                        string dipInp = Console.ReadLine();
                        dipInp = dipInp.ToLower();
                        if (dipInp == "y")
                        {
                            (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                            newice = new Cone("Cone", cat.Item1, cat.Item2, cat.Item3, true);
                        }
                        else if (dipInp == "n")
                        {
                            (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                            newice = new Cone("Cone", cat.Item1, cat.Item2, cat.Item3, false);
                        }
                        else
                        {
                            Console.WriteLine("Only [Y/N] or [y/n] accepted");
                            break;
                        }
                    }
                    else if (choiceInp == "waffle")
                    {
                        Console.WriteLine("Chosen Waffle");
                        Console.Write("Do you want a waffle flavour?(Red velvet, charcoal, or pandan) or original: ");
                        string wafInp = Console.ReadLine();
                        string waf = WaffleChoice(wafInp);
                        if (waf != null)
                        {
                            (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                            newice = new Waffle("Waffle", cat.Item1, cat.Item2, cat.Item3, waf);
                        }
                        else
                        {
                            Console.WriteLine($"Waffle flavour {wafInp} not available or invalid");
                        }
                        Console.WriteLine($"Your order: {newice}");
                        newOrd.AddIceCream(newice);
                        Console.Write("Do you wish to continue ordering? (Y/N): ");
                        string check = Console.ReadLine();
                        check = check.ToLower();
                        if (check == "y")
                        {
                            continue;
                        }
                        else if (check == "n")
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("invalid input");
                    }
                    Console.Write("Do you want to continue order(y or n)");
                string input = Console.ReadLine();
                if (input == "y") {
                    continue;
                
                }
                else if (input == "n") {
                    Console.WriteLine("order finished");
                    break;
                
                }
                else {
                    Console.WriteLine("invalid input");
                    Console.WriteLine("order finished");
                    break;




                }

            }

                if (result.CurrentOrder.IceCreamlist.Count != 0)
                {
                    Console.WriteLine($"\nOrder Number[{result.CurrentOrder.id}] is successfull");
                    string printed = $"Total Number of Ice Creams: {newOrd.IceCreamlist.Count}\n";
                    foreach (var v in newOrd.IceCreamlist)
                    {
                        printed += $"{v}\n";
                    }
                    Console.WriteLine(printed);
                    if (result.Rewards.tier == "Gold")
                    {
                        GoldQueueOrder.Enqueue(newOrd);
                        Console.WriteLine(result);
                    }
                    else
                    {
                        RegularQueueOrder.Enqueue(newOrd);
                    }

                }
                
            }
            else
            {
                Console.WriteLine("Customer Member Id not found");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid numeric value for customer ID.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    
}
    void Option5()
    {
    while (true)
    {
        try
        {

            Option1(DictCustomer);

            Console.Write("Select the customer: ");
            int idInp = Convert.ToInt32(Console.ReadLine());
            Customer
        ? result = Search(DictCustomer, idInp);
            if (result != null)
            {
                List<IceCream> currentorder = result.CurrentOrder.IceCreamlist;
                Console.WriteLine("current order");
                if (currentorder.Count == 0) {
                    Console.WriteLine("no current orders");

                }
                foreach (IceCream currenrorderice in currentorder)
                {
                    Console.WriteLine(currenrorderice);


                }
                Console.WriteLine("pass orders");
                if (result.OrderHistory.Count == 0)
                {
                    Console.WriteLine("no past orders");

                }
                foreach (Order pastorder in result.OrderHistory)
                {
                    Console.WriteLine("time recived");
                    Console.WriteLine(pastorder.timeRecieved);
                    Console.WriteLine("time fulfiled");
                    Console.WriteLine(pastorder.timeFulfilled);



                }
                break;
            }
            else { Console.WriteLine("invalid customer"); }




        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid numeric value for customer ID.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

}
    void Option6()

    {
    while (true)
    {
        try
        {


            Option1(DictCustomer);
            Console.Write("Select the customer: ");
            int idInp = Convert.ToInt32(Console.ReadLine());
            Customer
        ? result = Search(DictCustomer, idInp);

            if (result != null)
            {
                Console.WriteLine("Menu:\r\n1. Modify an existing ice cream in the order\r\n2. Add a new ice cream to the order\r\n3. Delete an existing ice cream from the order");
                Console.Write("Please enter the number corresponding to your choice: ");
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    int count = 1;
                    foreach (IceCream or in result.CurrentOrder.IceCreamlist)
                    {
                        Console.WriteLine($"[{count}]");
                        Console.WriteLine(or);
                        count++;


                    }
                    Console.Write("Enter a ice cream to modify: ");
                    try
                    {
                        int icecreanindex = Convert.ToInt32(Console.ReadLine());
                        result.CurrentOrder.Modifyicecream(icecreanindex);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("invalid input.");

                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("invalid input.");

                    }


                }
                else if (choice == "2")
                {
                    
                        
                        Makeicecream(result);
                        
                    

                }
                else if (choice == "2")
                {

                    int count = 1;
                    foreach (IceCream or in result.CurrentOrder.IceCreamlist)
                    {
                        Console.WriteLine($"[{count}]");
                        Console.WriteLine(or);
                        count++;


                    }
                    Console.Write("Enter a ice cream to remove: ");
                    try
                    {
                        int icecreanindex = Convert.ToInt32(Console.ReadLine());
                        result.CurrentOrder.DeleteIceCream(icecreanindex);
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine("invalid input.");

                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("invalid input.");

                    }







                }
                else { Console.WriteLine("invalid input"); }
            }
            else
            {
                Console.WriteLine("invalid custoemr.");

            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid numeric value for customer ID.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    }
    void Option7()
    {
        Order servingorder = null;
        if (GoldQueueOrder.Count != 0)
        {
            servingorder = GoldQueueOrder.Dequeue();
            

        }
        
        else 
        {
            servingorder = RegularQueueOrder.Dequeue();
        }
        double total = servingorder.CalcualteTotal();
        foreach (IceCream ice in servingorder.IceCreamlist)
        {
            Console.WriteLine(ice);

        }
        Console.WriteLine($"total cost: {total}");
        Customer servingcustomer = null;
        foreach (var custo in DictCustomer)
        {
            Customer customers = custo.Value;
            if (customers != null)
            {
                if (customers.CurrentOrder.id == servingorder.id)
                {
                    servingcustomer = customers;
                    break;


                }
                else
                {
                    continue;

                }
            }


        }
        Console.WriteLine($"membership teir: {servingcustomer.Rewards.tier}        points: {servingcustomer.Rewards.points}");
        if (servingcustomer.IsBirthday())
        {
            Console.WriteLine("happy birthday");
            double highest = 0;
            foreach (IceCream ice in servingorder.IceCreamlist)
            {
                if (ice.CalculatePrice() >= highest)
                {
                    highest = ice.CalculatePrice();

                }
                else
                {
                    continue;

                }

            }
            total -= highest;
            Console.WriteLine($"new total :{total}");



        }


        if (servingcustomer.Rewards.tier == "Ordinary")
        {
            IceCream firstice = servingorder.IceCreamlist[0];

            if (servingcustomer.Rewards.punchCard == 11)
            {
                Console.WriteLine("you have 11 punches on ur punch card.your first icecreamm is free");
                servingcustomer.Rewards.punchCard = 0;
                total -= firstice.CalculatePrice();




            }
            Console.WriteLine($"fianl toatal: {total}");
            int points = Convert.ToInt16(Math.Floor(total * 0.72));
            servingcustomer.Rewards.AddPoints(points);
            servingcustomer.CurrentOrder.timeFulfilled = DateTime.Now;
            servingcustomer.OrderHistory.Add(servingcustomer.CurrentOrder);
            servingcustomer.CurrentOrder = null;
            if (servingcustomer.Rewards.points >= 50)
            {
                servingcustomer.Rewards.tier = "Silver";

                Console.WriteLine("congrats u are now a silver teir member");

            }


            WriteIceCream(servingorder, servingcustomer.MemberId);
        }
        else
        {
            Console.WriteLine($"points: {servingcustomer.Rewards.points}");
            Console.Write("would u like to redeem some points(y/n): ");
            string redeem = Console.ReadLine();
            if (redeem == "y")
            {
                Console.Write("how much point would u like to redeem(1 point = 0.02): ");
                int point = Convert.ToInt16(Console.ReadLine());
                double discounted = point * 0.02;
                total -= discounted;

            }
            else if (redeem == "n") { }
            Console.WriteLine($"fianl toatal: {total}");
            int points = Convert.ToInt16(Math.Floor(total * 0.72));
            servingcustomer.Rewards.AddPoints(points);
            servingcustomer.CurrentOrder.timeFulfilled = DateTime.Now;
            servingcustomer.OrderHistory.Add(servingcustomer.CurrentOrder);
            servingcustomer.CurrentOrder = null;
            if (servingcustomer.Rewards.tier == "Silver")
            {
                if (servingcustomer.Rewards.points >= 100)
                {
                    servingcustomer.Rewards.tier = "gold";
                    Console.WriteLine("cpmgrats you are now a gold member!");



                }

            }

        }
        foreach (IceCream ice in servingorder.IceCreamlist)
        {
            servingcustomer.Rewards.Punch();
            if (servingcustomer.Rewards.punchCard == 10)
            {
                break;

            }

        }

    }
    // peck // 0Id,1MemberId,2TimeReceived,3TimeFulfilled,4Option,5Scoops,6Dipped,7WaffleFlavour,8Flavour1,9Flavour2,10Flavour3,11Topping1,12Topping2,13Topping3,14Topping4
    void WriteIceCream(Order order, int id)
    {
        foreach (var v in order.IceCreamlist)
        {
            using (StreamWriter sw = new StreamWriter("orders.csv", true))
            {
                string flavstr = "";
                if (v.Flavours.Count > 0)
                {
                    if (v.Toppings.Count == 1)
                    {
                        flavstr = string.Join(",", v.Flavours[0].Type, "", "");
                    }
                    else if (v.Toppings.Count == 2)
                    {
                        flavstr = string.Join(",", v.Flavours[0].Type, v.Flavours[1].Type, "");
                    }
                    else if (v.Toppings.Count == 3)
                    {
                        flavstr = string.Join(",", v.Flavours[0].Type, v.Flavours[1].Type, v.Flavours[2].Type);
                    }
                }
                string topstr = "";
                if (v.Toppings.Count > 0)
                {
                    if (v.Toppings.Count == 1)
                    {
                        topstr = string.Join(",", v.Toppings[0].Type, "", "", "");
                    }
                    else if (v.Toppings.Count == 2)
                    {
                        topstr = string.Join(",", v.Toppings[0].Type, v.Toppings[1].Type, "", "");
                    }
                    else if (v.Toppings.Count == 3)
                    {
                        topstr = string.Join(",", v.Toppings[0].Type, v.Toppings[1].Type, v.Toppings[2].Type, "");
                    }
                    else if (v.Toppings.Count == 4)
                    {
                        topstr = string.Join(",", v.Toppings[0].Type, v.Toppings[1].Type, v.Toppings[2].Type, v.Toppings[3].Type);
                    }
                }
                string? row;
                row = string.Join(",", order.id, id, order.timeRecieved, order.timeFulfilled, v.Option, v.Scoops);
                if (v is Cup)
                {
                    row = string.Join(",", order.id, id, order.timeRecieved, order.timeFulfilled, v.Option, v.Scoops, "", "", flavstr, topstr);
                }
                else if (v is Cone)
                {
                    Cone cone = (Cone)v;
                    row = string.Join(",", order.id, id, order.timeRecieved, order.timeFulfilled, v.Option, v.Scoops, cone.Dipped, "", flavstr, topstr);
                }
                else if (v is Waffle)
                {
                    Waffle waf = (Waffle)v;
                    row = string.Join(",", order.id, id, order.timeRecieved, order.timeFulfilled, v.Option, v.Scoops, "", waf.WaffleFlavour, flavstr, topstr);
                }
                sw.WriteLine(row);
            }
        }

    }
    void Option8()
    {
        void Option8()
        {
            Console.Write("Enter the year: ");
            int inputYear = int.Parse(Console.ReadLine());

            // Assuming DictCustomer is a Dictionary<string, customer>
            double[] monthlyTotals = new double[12]; // One entry for each month

            foreach (var custo in DictCustomer)
            {
                Customer custom = custo.Value;

                // Group orders by month
                var ordersByMonth = custom.OrderHistory
                    .Where(or => or.timeFulfilled.HasValue && or.timeFulfilled.Value.Year == inputYear)
                    .GroupBy(or => or.timeFulfilled.Value.Month);

                // Iterate through each month
                foreach (var monthGroup in ordersByMonth)
                {
                    int month = monthGroup.Key;

                    // Retrieve IceCreamList for the month
                    List<IceCream> iceCreamList = monthGroup
                        .SelectMany(or => or.IceCreamlist)
                        .ToList();

                    // Calculate total for the month
                    double monthTotal = iceCreamList.Sum(iceCream => iceCream.CalculatePrice());
                    monthlyTotals[month - 1] += monthTotal; // Adjust month index to 0-based

                    // Do something with the monthTotal or other logic
                    Console.WriteLine($"Month: {month}, Ice Cream Total: {monthTotal}");
                }
            }

            // Print the total for each month
            for (int i = 0; i < monthlyTotals.Length; i++)
            {
                Console.WriteLine($"Total for {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i + 1)}: {monthlyTotals[i]}");
            }
        }

    }

    Dictionary<int, string> wafflelist = new Dictionary<int, string>();
    void initwaffle(Dictionary<int, string> wafflelist)
    {
        wafflelist.Add(1, "Original");
        wafflelist.Add(2, "Red velvet");
        wafflelist.Add(3, "charcoal");
        wafflelist.Add(4, "pandan waffle");




    }
    void displaywaffle(Dictionary<int, string> wafflelist)
    {
        foreach (var kpv in wafflelist)
        {
            Console.WriteLine($"[{kpv.Key}]: {kpv.Value,-10}");

        }

    }




    void Makeicecream(Customer
     result)
    {
        IceCream newIceCream = null;
        List<Flavour> flavlist = new List<Flavour>();
        List<Topping> toplist = new List<Topping>();
        Dictionary<int, string> wafflelist = new Dictionary<int, string>();
        initwaffle(wafflelist);
    while (true)
    {
        Console.Write("Enter their ice cream order type (Cup, Cone or Waffle): ");
        string type = Console.ReadLine();

        if (type == "cup")
        {
            while (true)
            {
                Console.Write("Enter number of scoops[1-3]: ");



                int newscp = Convert.ToInt16(Console.ReadLine());
                if (newscp > 0 && newscp < 3)
                {
                    DisplayFlavours(DictFlavour);
                    for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
                    {
                        Console.Write($"Enter flavour number {scoopIndex} : ");
                        int newflav = Convert.ToInt32(Console.ReadLine());
                        Flavour addflact = DictFlavour[newflav];
                        if (addflact != null)
                        {
                            flavlist.Add(addflact);
                        }
                        else
                        {
                            foreach (var flav in flavlist)
                            {
                                if (flav.Type == addflact.Type)
                                {
                                    flav.Quantity += 1;


                                }


                            }







                        }



                    }
                    
                    while (true)
                    {
                        Console.Write("Enter number of toppings[1-4]: ");


                        int newtop = Convert.ToInt16(Console.ReadLine());
                        if (newtop >= 1 && newtop <= 4)
                        {
                            DisplayToppings(DictTopping);
                            for (int topIndex = 1; topIndex <= newtop; topIndex++)
                            {
                                Console.Write($"Enter toping number {topIndex} : ");
                                int addtop = Convert.ToInt32(Console.ReadLine());
                                Topping toppingtolist = DictTopping[addtop];
                                toplist.Add(toppingtolist);


                            }
                            newIceCream = new Cup("cup", newscp, flavlist, toplist);
                            result.CurrentOrder.AddIceCream(newIceCream);
                            

                        }
                        else
                        {
                            Console.WriteLine("invalid input");

                        }
                    }

                }
                else
                {
                    Console.WriteLine("invalid input.");


                }






            }

        }


        else if (type == "waffle")
        {
            Console.Write("Enter number of scoops: ");


            int newscp = Convert.ToInt16(Console.ReadLine());

            DisplayFlavours(DictFlavour);
            for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
            {
                Console.Write($"Enter flavour number {scoopIndex} : ");
                int newflav = Convert.ToInt32(Console.ReadLine());
                Flavour addflact = DictFlavour[newflav];
                if (addflact != null)
                {
                    flavlist.Add(addflact);
                }
                else
                {
                    foreach (var flav in flavlist)
                    {
                        if (flav.Type == addflact.Type)
                        {
                            flav.Quantity += 1;


                        }


                    }







                }



            }
            Console.Write("Enter number of toppings: ");


            int newtop = Convert.ToInt16(Console.ReadLine());
            DisplayToppings(DictTopping);
            for (int topIndex = 1; topIndex <= newtop; topIndex++)
            {
                Console.Write($"Enter toping number {topIndex} : ");
                int addtop = Convert.ToInt32(Console.ReadLine());
                Topping toppingtolist = DictTopping[addtop];
                toplist.Add(toppingtolist);


            }
            displaywaffle(wafflelist);
            Console.Write("Enter waffle flavour: ");
            int wafflenum = Convert.ToInt32(Console.ReadLine());
            string waffeflav = wafflelist[wafflenum];
            newIceCream = new Waffle("waffle", newscp, flavlist, toplist, waffeflav);
            result.CurrentOrder.AddIceCream(newIceCream);

        }
        else if (type == "cone")
        {
            Console.Write("Enter number of scoops: ");


            int newscp = Convert.ToInt16(Console.ReadLine());

            DisplayFlavours(DictFlavour);
            for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
            {
                Console.Write($"Enter flavour number {scoopIndex} : ");
                int newflav = Convert.ToInt32(Console.ReadLine());
                Flavour addflact = DictFlavour[newflav];
                if (addflact != null)
                {
                    flavlist.Add(addflact);
                }
                else
                {
                    foreach (var flav in flavlist)
                    {
                        if (flav.Type == addflact.Type)
                        {
                            flav.Quantity += 1;


                        }


                    }







                }



            }
            Console.Write("Enter number of toppings: ");


            int newtop = Convert.ToInt16(Console.ReadLine());
            DisplayToppings(DictTopping);
            for (int topIndex = 1; topIndex <= newtop; topIndex++)
            {
                Console.Write($"Enter toping number {topIndex} : ");
                int addtop = Convert.ToInt32(Console.ReadLine());
                Topping toppingtolist = DictTopping[addtop];
                toplist.Add(toppingtolist);


            }
            Console.Write("Do you want your cone dipped?(Y/N):");
            string dipped = Console.ReadLine();
            if (dipped.ToLower() == "y")
            {
                newIceCream = new Cone("cone", newscp, flavlist, toplist, true);


            }
            else if (dipped.ToLower() == "n")
            {
                newIceCream = new Cone("cone", newscp, flavlist, toplist, false);


            }

            result.CurrentOrder.AddIceCream(newIceCream);

        }
        else {
            Console.WriteLine("invalid input");


        }


    }

    }
   


