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
// Init the Dict and Queues
Dictionary<int, Customer>DictCustomer = new Dictionary<int, Customer>();
Queue<Order> GoldQueueOrder = new Queue<Order>();
Queue<Order> RegularQueueOrder = new Queue<Order>();
Dictionary<int, Flavour> DictFlavour = new Dictionary<int, Flavour>();
Dictionary<int, Topping> DictTopping = new Dictionary<int, Topping>();

// Error Handling incase process file errors
try
{
    InitCustomer("customers.csv");
    InitFlavours("flavours.csv", DictFlavour);
    InitToppings("toppings.csv", DictTopping);
    InitOrders("orders.csv");
}
catch (IOException ioex)
{
    throw new IOException("An error occurred while processing the files.", ioex);
}
catch (Exception ex)
{
    throw new Exception("An generic error ocurred from file reading.");
}



// Loop of Options

do
{
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
        Option7();
    }else if (usrInp == "8")
    {
        Option8();
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
//==========================================================
// Student Number : s10257702
// Student Name : khoo jia xiang
//==========================================================
//display
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
// Reading of customers
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
// Read Flavours and add it to a dict
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
// Read toppings and add it to a dict
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
// Reading of Ice Cream Order History
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
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
// Reading of icecreams FORMAT from the history
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
// Student Number : s10257702
// Student Name : khoo jia xiang
//==========================================================
//find order id
int maxorderid() {
    int maxid = 0;
    foreach(var i in DictCustomer) { 
        Customer customer = i.Value;
        foreach (var or in customer.OrderHistory) {
            if (or.id >= maxid)
            {
                maxid = or.id + 1;
            }
            else {
                continue;
            }
        }
        if (customer.CurrentOrder != null)
        {
            if (customer.CurrentOrder.id >= maxid)
            {

                maxid = customer.CurrentOrder.id;
            }
        }
        
    }
    
    return maxid;
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

void Option1(Dictionary<int, Customer> DictCustomer) 
{
    // Title
    Console.WriteLine($"{"Name",-12}{"Member ID",-12}{"DateofBirth",-15}{"MemberShip",-15}{"Points",-7}{"Punches",-2}");
    foreach (var kvp in DictCustomer)
    {
        // Writing of a single customer
        Console.WriteLine($"{kvp.Value.Name,-12}{kvp.Value.MemberId,-12}{kvp.Value.Dob,-15:dd/MM/yyyy}{kvp.Value.Rewards.tier,-15}{kvp.Value.Rewards.points,-7}{kvp.Value.Rewards.punchCard}");
    }
}
//==========================================================
// Student Number : s10257702
// Student Name : khoo jia xiang
//==========================================================
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
            // If the id is not same with an exisitng customer it can be added
            if (!same)
            {
                Console.Write("Enter their Date Of Birth in DD/MM/YYYY: ");
                DateTime dob = Convert.ToDateTime(Console.ReadLine());
                Customer newCustomer = new Customer(nameInp, idInp, dob);
                Console.WriteLine("Their registration customer details");
                Console.WriteLine($"Name: {newCustomer.Name,-10} Member ID:{newCustomer.MemberId,-10} DateofBirth: {newCustomer.Dob,-10:dd/MM/yyyy}");
                // Set customer a point card with default info
                PointCard newPC = new PointCard(0, 0);
                newPC.tier = "Ordinary";
                newCustomer.Rewards = newPC;
                // Write the customer info in customers.csvs
                using (StreamWriter sw = new StreamWriter("customers.csv", true))
                {
                    string? row;
                    row = string.Join(",", newCustomer.Name, newCustomer.MemberId, $"{newCustomer.Dob:dd/MM/yyyy}", newCustomer.Rewards.tier, newCustomer.Rewards.points, newCustomer.Rewards.punchCard);
                    sw.WriteLine(row);
                }
                DictCustomer.Add(newCustomer.MemberId, newCustomer);
                Console.WriteLine("Registration Successfull");
                // It breaks when successful if not it will repeat
                break;
            }
            else
            {
                Console.WriteLine("Member ID Number used\nPlease use another Member ID Number");
            }
        }
        catch (FormatException)
        {
            // Only error possible here is date error
            Console.WriteLine("Invalid format. Please enter a valid format");
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
// Search Customers based on Member ID
static Customer? Search(Dictionary<int, Customer> sDict, int userInp)
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
// Make the flavour list and topping list for the IC
(int, List<Flavour>, List<Topping>)? IceCreamAdd(Dictionary<int, Flavour> df, Dictionary<int, Topping> dt)
{
    // init list for parsing data
    List<Flavour> flavList = new List<Flavour>();
    List<Topping> topList = new List<Topping>();
    Console.Write("Enter number of scoops [1-3]: ");
    // init all variables for error handling and more
    int scoops = 0;
    int topCount = 0;
    int flvIndex = 0;
    int topIndex = 0;
    bool error =  false;
    try
    {
        scoops = Convert.ToInt32(Console.ReadLine());
        if (scoops <= 3 && scoops > 0)
        {
            for (int i = 1; i < scoops + 1; i++)
            {
                flvIndex = 0;
                DisplayFlavours(DictFlavour);
                Console.Write($"Choose the flavour of scoop [{i}]: ");
                flvIndex = Convert.ToInt32(Console.ReadLine());
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
                else
                {
                    Console.WriteLine("No Such Flavour Number Available");
                    return null;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid number of scoops | Only 1 to 3 scoops");
            error = true;
        }
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Invalid format only numbers [1-3] scoops and [1-6] flavours");
        error = true;
    }
    catch (Exception ex) when(!(scoops <= 3 && scoops > 0))
    {
        Console.WriteLine("Invalid Number of Scoops | Only 1 to 3 Scoops");
        error = true;
    }
    if(!(scoops <= 3 && scoops > 0) || error)
    {
        return null;
    }
    try
    {
        Console.Write("Enter number of toppings [1-4]: ");
        topCount = Convert.ToInt32(Console.ReadLine());

        if (topCount > 0 && topCount <= 4)
        {
            for (int i = 1; i < topCount + 1; i++)
            {
                DisplayToppings(DictTopping);
                Console.Write($"Choose the [{i}] topping: ");
                topIndex = Convert.ToInt32(Console.ReadLine());
                if (dt.ContainsKey(topIndex))
                {
                    topList.Add(dt[topIndex]);
                }
                else
                {
                    Console.WriteLine("No Such Topping Number Available");
                    error = true;
                }
            }
        }
        else if(topCount == 0)
        {
            Console.WriteLine("No Toppings Added");
            error = true;
        }
        else
        {
            Console.WriteLine("Invalid Number Of Toppings | Only 0 to 4 Toppings");
            error = true;
        }
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Invalid format only numbers [0-4] toppings and [1-4] flavours");
        error = true;
    }
    catch (Exception ex) when (!(topCount <= 4 && topCount >= 0))
    {
        Console.WriteLine("Invalid number of toppings | Only 0 to 4 toppings");
        error = true;
    }
    if(!(topCount <= 4 && topCount >= 0) || error)
    {
        return null;
    }
    // If preconditions meet and no error it will send the data
    if((scoops <= 3 && scoops > 0) && (topCount <= 4 && topCount >= 0) || !(error))
    {
        return (scoops, flavList, topList);
    }
    else
    {
        Console.WriteLine("Your inputs are Invalid");
        return null;
    }
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
        // Select and find the customer
        Console.Write("Select the customer: ");
        int idInp = Convert.ToInt32(Console.ReadLine());
        Customer? result = Search(DictCustomer, idInp);
        if (result != null)
        {
            Console.WriteLine("Found Customer ");
            Order newOrd = result.CurrentOrder;

            newOrd.id = maxorderid() + 1;
            Console.WriteLine(newOrd.id);
            newOrd.timeRecieved = DateTime.Now;
            while (true)
            {
                Console.Write("Enter their ice cream order type (Cup, Cone or Waffle): ");

                string choiceInp = Console.ReadLine();
                // Lower to prevent case sensitive
                choiceInp = choiceInp.ToLower();
                // Option Cup
                if (choiceInp == "cup")
                {
                    Console.WriteLine("Chosen Cup");
                    (int, List<Flavour>, List<Topping>)? cat = IceCreamAdd(DictFlavour, DictTopping);
                    if (cat != null)
                    {
                        newice = new Cup("Cup", cat.Value.Item1, cat.Value.Item2, cat.Value.Item3);
                    }
                    else
                    {
                        newice = null;
                        break;
                    }
                }
                // Option Cone
                else if (choiceInp == "cone")
                {
                    Console.WriteLine($"Chosen Cone");
                    Console.Write("Do you want your cone dipped?(Y/N): ");
                    string dipInp = Console.ReadLine();
                    dipInp = dipInp.ToLower();
                    if (dipInp == "y")
                    {
                        (int, List<Flavour>, List<Topping>)? cat = IceCreamAdd(DictFlavour, DictTopping);
                        if (cat != null)
                        {
                            newice = new Cone("Cone", cat.Value.Item1, cat.Value.Item2, cat.Value.Item3, true);
                        }
                        else
                        {
                            newice = null;
                            break;
                        }
                    }
                    else if (dipInp == "n")
                    {
                        (int, List<Flavour>, List<Topping>)? cat = IceCreamAdd(DictFlavour, DictTopping);
                        if (cat != null)
                        {
                            newice = new Cone("Cone", cat.Value.Item1, cat.Value.Item2, cat.Value.Item3, false);
                        }
                        else
                        {
                            newice = null;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Only [Y/N] or [y/n] accepted");
                        break;
                    }
                }
                // Option Waffle
                else if (choiceInp == "waffle")
                {
                    Console.WriteLine("Chosen Waffle");
                    Console.Write("Do you want a waffle flavour?(Red velvet, charcoal, or pandan) or original: ");
                    string wafInp = Console.ReadLine();
                    string waf = WaffleChoice(wafInp);
                    if (waf != null)
                    {
                        (int, List<Flavour>, List<Topping>)? cat = IceCreamAdd(DictFlavour, DictTopping);
                        if(cat != null)
                        {
                            newice = new Waffle("Waffle", cat.Value.Item1, cat.Value.Item2, cat.Value.Item3, waf);
                        }
                        else
                        {
                            newice = null;
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Waffle flavour {wafInp} not available or invalid input");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                    break;
                }
                // If no IceCream wont add to the order
                if(newice!= null)
                {
                    Console.WriteLine($"Your order: {newice}");
                    newOrd.AddIceCream(newice);
                    Console.Write("Do you wish to continue ordering? [Y/N]: ");
                    string check = Console.ReadLine();
                    // Prevent case sensitive
                    check = check.ToLower();
                    if (check == "y")
                    {
                        continue;
                    }
                    else if (check == "n")
                    {
                        Console.WriteLine("Order saved");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input [Y/N] or [y/n] only\nOrder will be stopped and saved");
                        break;
                    }
                }
                else
                {
                    // Prints that your Ice Cream had error
                    Console.WriteLine("Your IceCream is invalid");
                    break;
                }
            }
            // If no ice cream in order wont enqueue the order
            if (result.CurrentOrder.IceCreamlist.Count != 0)
            {
                Console.WriteLine($"\nOrder Number[{result.CurrentOrder.id}] is successfull");
                string printed = $"Total Number of Ice Creams: {newOrd.IceCreamlist.Count}\n";
                double totalcost = 0 ;
                foreach (var v in newOrd.IceCreamlist)
                {
                    printed += $"{v}\n";
                    totalcost += v.CalculatePrice();
                }
                Console.WriteLine(printed);
                Console.WriteLine($"Total Cost: ${totalcost,-5:0.00}");
                if (result.Rewards.tier == "Gold")
                {
                    GoldQueueOrder.Enqueue(newOrd);
                }
                else
                {
                    RegularQueueOrder.Enqueue(newOrd);
                }

            }
            else
            {
                // no Ice Cream user reenter data
                Console.WriteLine("Error occured when making IceCream\nPlease restart your ordering process");
            }
                
        }
        else
        {
            // No such user
            Console.WriteLine("Customer Member ID not found");
        }
    }
    // Formating error
    catch (FormatException)
    {
        Console.WriteLine("Invalid input. Please enter a valid numeric value for customer ID.");
    }
    // General Error
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}
//==========================================================
// Student Number : s10257702
// Student Name : khoo jia xiang
//==========================================================
void Option5()
{
    Option1(DictCustomer);
    while (true)
    {
        try
        {

            

            Console.Write("Select the customer: ");
            int idInp = Convert.ToInt32(Console.ReadLine());
            Customer
        ? result = Search(DictCustomer, idInp);
            if (result != null)
            {
                int currentcount =0;
                List<IceCream> currentorder = null;
                if (result.CurrentOrder.IceCreamlist != null)
                {
                    currentorder = result.CurrentOrder.IceCreamlist;

                }
                else { 
                    currentcount= 0;
                
                }

                
                Console.WriteLine("current order");
                if (currentcount == 0)
                {
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
//==========================================================
// Student Number : s10257702
// Student Name : khoo jia xiang
//==========================================================
void Option6()

    {
        Option1(DictCustomer);
    while (true)
    {
        try
        {


            
            Console.Write
("Select the customer: ");
            int idInp = Convert.ToInt32(Console.ReadLine());
            Customer
        ? result = Search(DictCustomer, idInp);

            
            
                if (result != null)
                {
                while (true)
                {
                    Console.WriteLine("Menu:\r\n1. Modify an existing ice cream in the order\r\n2. Add a new ice cream to the order\r\n3. Delete an existing ice cream from the order\r\n4. Exit");
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
                            Console.WriteLine($"Modified ice cream: \r\n{result.CurrentOrder.IceCreamlist[icecreanindex - 1]}");

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
                        int count = 1;
                        int icenum = result.CurrentOrder.IceCreamlist.Count;

                        while (true)
                        {
                            Makeicecream(result);
                            Console.Write("do you want to continue order(y 0r n): ");
                            string input = Console.ReadLine();
                            if (input == "n")
                            {
                                break;

                            }
                            else if (input == "y")
                            {
                                count++;
                                continue;



                            }
                            else {
                                Console.WriteLine("invalid input");
                                break;

                            }


                        }
                        Console.WriteLine("orders added:");
                        for (int i = 1; i <= count; i++)
                        {
                            Console.WriteLine(result.CurrentOrder.IceCreamlist[icenum - 1]);




                        }









                    }
                    else if (choice == "3")
                    {
                        while (true)
                        {
                            try
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
                                    Console.WriteLine("new order:");
                                    int count1 = 1;
                                    foreach (IceCream or in result.CurrentOrder.IceCreamlist)
                                    {
                                        Console.WriteLine($"[{count1}]");
                                        Console.WriteLine(or);
                                        count++;


                                    }
                                    Console.WriteLine("---------current order-------");
                                    int count2 = 1;
                                    foreach (IceCream or in result.CurrentOrder.IceCreamlist)
                                    {
                                        Console.WriteLine($"[{count2}]");
                                        Console.WriteLine(or);
                                        count++;


                                    }

                                    break;
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
                            catch (Exception)
                            {
                                Console.WriteLine("invliad input.");

                            }
                        }









                    }
                    else if (choice == "4")
                    {
                        break;

                    }

                    else { Console.WriteLine("invalid input"); }
                    if (result.Rewards.tier == "Gold")
                    {
                        Queue<Order> temp = new Queue<Order>();
                        while (GoldQueueOrder.Count > 0) { 
                            Order order = GoldQueueOrder.Dequeue();
                            if (result.CurrentOrder.id == order.id)
                            {
                                order = result.CurrentOrder;
                            }
                            temp.Enqueue(order);

                        }
                        while (temp.Count > 0) {
                            Order neworders = temp.Dequeue();
                            GoldQueueOrder.Enqueue(neworders);




                        }

                    }
                    else {
                        Queue<Order> temp = new Queue<Order>();
                        while (RegularQueueOrder.Count > 0)
                        {
                            Order order = RegularQueueOrder.Dequeue();
                            if (result.CurrentOrder.id == order.id)
                            {
                                order = result.CurrentOrder;
                            }
                            temp.Enqueue(order);

                        }
                        while (temp.Count > 0)
                        {
                            Order neworders = temp.Dequeue();
                            RegularQueueOrder.Enqueue(neworders);




                        }


                    }
                }
                break;
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
//==========================================================
// Student Number : s10257702
// Student Name : khoo jia xiang
//==========================================================
void Option7()
    {
    
    while(true){
        try
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
            int ordercount = servingorder.IceCreamlist.Count;
            
            
            Customer servingcustomer = null;
            foreach (var custo in DictCustomer)
            {
                Customer customers = custo.Value;
                if (customers != null)
                {
                    if (customers.CurrentOrder != null)
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
            }
            Console.WriteLine($"name:{servingcustomer.Name}     Teir:{servingcustomer.Rewards.tier}     points:{servingcustomer.Rewards.points}      punch card:{servingcustomer.Rewards.punchCard}");
            foreach (IceCream ice in servingorder.IceCreamlist)
            {
                Console.WriteLine(ice);

            }
            Console.WriteLine($"membership teir: {servingcustomer.Rewards.tier}        points: {servingcustomer.Rewards.points}");
            Console.WriteLine($"current cost: ${total:0.00}");
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
                Console.WriteLine($"fianl toatal: ${total:0.00}");
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


                
            }
            else
            {
                IceCream firstice = servingorder.IceCreamlist[0];
                if (servingcustomer.Rewards.punchCard == 10)
                {
                    Console.WriteLine("you have 11 punches on ur punch card.your first icecreamm is free");
                    servingcustomer.Rewards.punchCard = 0;
                    total -= firstice.CalculatePrice();
                    ordercount--;
                    Console.WriteLine($"toatal: ${total:0.00}");




                }
                if (total > 0)
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
                        Console.WriteLine($"curent toatal: ${total:0.00}");


                    }
                    else if (redeem == "n") { }
                    Console.WriteLine($"fianl toatal: ${total:0.00}");
                    
                }
                int points = Convert.ToInt16(Math.Floor(total * 0.72));
                servingcustomer.Rewards.AddPoints(points);
                servingcustomer.CurrentOrder.timeFulfilled = DateTime.Now;
                WriteIceCream(servingorder, servingcustomer.MemberId);
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
            break;
            



            
            
        }
        catch(InvalidOperationException) {
            Console.WriteLine();
            Console.WriteLine("no current orders");
            break;

        }
    }
    

    }
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
// peck // 0Id,1MemberId,2TimeReceived,3TimeFulfilled,4Option,5Scoops,6Dipped,7WaffleFlavour,8Flavour1,9Flavour2,10Flavour3,11Topping1,12Topping2,13Topping3,14Topping4
void WriteIceCream(Order order, int id)
    {
        using (StreamWriter sw = new StreamWriter("orders.csv", true))
        {
            foreach (var v in order.IceCreamlist)
            {
            
                string flavstr = "";
                if (v.Flavours.Count > 0)
                {
                    if (v.Flavours.Count == 1)
                    {
                        flavstr = string.Join(",", v.Flavours[0].Type, "", "");
                    }
                    else if (v.Flavours.Count == 2)
                    {
                        flavstr = string.Join(",", v.Flavours[0].Type, v.Flavours[1].Type, "");
                    }
                    else if (v.Flavours.Count == 3)
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
            void 
Option8()
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

    }
//==========================================================
// Student Number : s10257702
// Student Name : khoo jia xiang
//==========================================================

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
//==========================================================
// Student Number : s10257702
// Student Name : khoo jia xiang
//==========================================================



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
                        try
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
                        catch {
                            Console.WriteLine("invalid input");
                            scoopIndex--;


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
                                try
                                {
                                    Console.Write($"Enter toping number {topIndex} : ");
                                    int addtop = Convert.ToInt32(Console.ReadLine());
                                    Topping toppingtolist = DictTopping[addtop];
                                    toplist.Add(toppingtolist);
                                }
                                catch {
                                    Console.WriteLine("invalid input");
                                    
                                    topIndex --;

                                }


                            }
                            newIceCream = new Cup("cup", newscp, flavlist, toplist);
                            result.CurrentOrder.AddIceCream(newIceCream);
                            break;


                        }
                        else
                        {
                            Console.WriteLine("invalid input");

                        }
                    }
                    break;



                }
                else
                {
                    Console.WriteLine("invalid input.");


                }
                






            }
            break;

        }


        else if (type == "waffle")
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
                        try
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
                        catch {
                            Console.WriteLine("invalid input");
                            scoopIndex--;

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
                                try
                                {
                                    Console.Write($"Enter toping number {topIndex} : ");
                                    int addtop = Convert.ToInt32(Console.ReadLine());
                                    Topping toppingtolist = DictTopping[addtop];
                                    toplist.Add(toppingtolist);
                                }
                                catch {
                                    Console.WriteLine("invalid input");
                                    topIndex --;

                                }


                            }
                            while (true)
                            {
                                try
                                {
                                    displaywaffle(wafflelist);
                                    Console.Write("Enter waffle flavour: ");
                                    int wafflenum = Convert.ToInt32(Console.ReadLine());
                                    string waffeflav = wafflelist[wafflenum];
                                    newIceCream = new Waffle("waffle", newscp, flavlist, toplist, waffeflav);
                                    result.CurrentOrder.AddIceCream(newIceCream);
                                    break;
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    Console.WriteLine("invalid input");

                                }
                                catch (Exception) {
                                    Console.WriteLine("invalid input ");

                                }
                            }
                            break;
                            


                        }
                        else
                        {
                            Console.WriteLine("invalid input");

                        }
                        


                    }
                    break;
                }
                else { Console.WriteLine("invalid input"); }
                

            }
            break;
            
            

        }
        else if (type == "cone")
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
                            while (true)
                            {
                                Console.Write("Do you want your cone dipped?(Y/N):");
                                string dipped = Console.ReadLine();
                                if (dipped.ToLower() == "y")
                                {
                                    newIceCream = new Cone("cone", newscp, flavlist, toplist, true);
                                    result.CurrentOrder.AddIceCream(newIceCream);
                                    break;


                                }
                                else if (dipped.ToLower() == "n")
                                {
                                    newIceCream = new Cone("cone", newscp, flavlist, toplist, false);
                                    result.CurrentOrder.AddIceCream(newIceCream);
                                    break;


                                }
                                else
                                {
                                    Console.WriteLine("invalid input");

                                }
                            }
                            break;


                        }

                    }
                    break;
                }
                else {
                    Console.WriteLine("invalid input");

                }
                
                
                           
                
            }
            break;
            

        }
        else {
            Console.WriteLine("invalid input");
            continue;


        }
        


    }

    }
   


