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


//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
Dictionary<int, customer> DictCustomer = new Dictionary<int, customer>();
Queue<Order> GoldQueueOrder = new Queue<Order>();
Queue<Order> RegularQueueOrder = new Queue<Order>();
Dictionary<int, Flavour> DictFlavour = new Dictionary<int, Flavour>();
Dictionary<int, Topping> DictTopping = new Dictionary<int, Topping>();

InitCustomer("customers.csv");
InitFlavours("flavours.csv",DictFlavour);
InitToppings("toppings.csv",DictTopping);
InitOrders("orders.csv");
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
    }else if (usrInp == "7")
    {
        Option7();
    }else if (usrInp == "8")
    {
        Option8();
    }
    else if(usrInp == "-1")
    {
        Console.WriteLine("Thanks for using the application!");
        break;
    }
    else
    {
        Console.WriteLine("Please enter a appriopriate input [1 - 7] Option or [-1] to Exit");
    }
}
void intialdisplay() {
    Console.WriteLine();

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
            customer newCustomer = new customer(rowList[0], Convert.ToInt32(rowList[1]), Convert.ToDateTime(rowList[2]));
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
            foreach (var v in DictCustomer)
            {
                if (v.Key == Convert.ToInt32(rowList[1]))
                {
                    bool addOrderHist = false;
                    // 4Option,5Scoops,6Dipped,7WaffleFlavour,8Flavour1,9Flavour2,10Flavour3,11Topping1,12Topping2,13Topping3,14Topping4
                    IceCream newIC = IceCreamRead(rowList[4], Convert.ToInt32(rowList[5]), rowList[6], rowList[7], rowList[8], rowList[9], rowList[10], rowList[11], rowList[12], rowList[13], rowList[14]);
                    order.AddIceCream(newIC);
                    v.Value.OrderHistory.Add(order);
                    addOrderHist = true;
                }
            }
        }
    }
}
// 4Option,5Scoops,6Dipped,7WaffleFlavour,8Flavour1,9Flavour2,10Flavour3,11Topping1,12Topping2,13Topping3,14Topping4
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

void Option1(Dictionary<int, customer> DictCustomer) 
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
        customer customer = kvp.Value;
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
    Console.WriteLine("Registration of a new customer");
    Console.Write("Enter their Name: ");
    string nameInp = Console.ReadLine();
    Console.Write("Enter their ID Number: ");
    int idInp = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter their Date Of Birth in DD/MM/YYYY: ");
    DateTime dob = Convert.ToDateTime(Console.ReadLine());
    bool same = false;
    foreach(var kvp in DictCustomer)
    {
        if(kvp.Key == idInp)
        {
            same = true;
        }
    }
    if (!same)
    {
        customer newCustomer = new customer(nameInp, idInp, dob);
        Console.WriteLine("Their registration customer details");
        Console.WriteLine($"Name: {newCustomer.Name,-10} Member ID:{newCustomer.MemberId,-10} DateofBirth: {newCustomer.Dob,-10:dd/MM/yyyy}");
        PointCard newPC = new PointCard(0, 0);
        newPC.tier = "Ordinary";
        newCustomer.Rewards = newPC;

        using (StreamWriter sw = new StreamWriter("customers.csv", true))
        {
            string? row;
            row = string.Join(",", newCustomer.Name, newCustomer.MemberId, $"{newCustomer.Dob:dd/MM/yyyy}",newCustomer.Rewards.tier,newCustomer.Rewards.points,newCustomer.Rewards.punchCard);
            sw.WriteLine(row);
        }
        DictCustomer.Add(newCustomer.MemberId, newCustomer);
        Console.WriteLine("Registration Successfull");
    }
    else
    {
        Console.WriteLine("Member ID Number used\nPlease use another Member ID Number");
    }

}
//==========================================================
// Student Number : S10262791
// Student Name : Asyiq Nuruddin
//==========================================================
static customer? Search(Dictionary<int,customer> sDict, int userInp)
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
(int, List<Flavour>, List<Topping>) IceCreamAdd(Dictionary<int,Flavour> df, Dictionary<int, Topping> dt)
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
    if(wafInp != null)
    {
        if(wafInp == "red velvet")
        {
            return "Red Velvet";
        }else if(wafInp == "charcoal")
        {
            return "Charcoal";
        }
        else if(wafInp == "pandan")
        {
            return "Pandan";
        }else if (wafInp == "original")
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
    // show Customer Details
    Option1(DictCustomer);
    IceCream newice = null;
    Console.Write("Select the customer: ");
    int idInp = Convert.ToInt32(Console.ReadLine());
    customer? result = Search(DictCustomer, idInp);
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
            }
            else
            {
                break;
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
            else
            {
                Console.WriteLine("Invalid Input (Y/N) or (y/n) only\nOrder will be stopped");
            }
        }

        if(result.CurrentOrder.IceCreamlist.Count != 0)
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
void Option5() {
    Option1(DictCustomer);
    Console.Write("Select the customer: ");
    int idInp = Convert.ToInt32(Console.ReadLine());
    customer? result = Search(DictCustomer, idInp);
    if (result != null)
    {
        List<IceCream> currentorder = result.CurrentOrder.IceCreamlist;
        Console.WriteLine("current order");
        Console.WriteLine(result.CurrentOrder.timeRecieved);
        foreach (IceCream currenrorderice in currentorder)
        {
            Console.WriteLine(currenrorderice);


        }
        Console.WriteLine("pass orders");
        foreach (Order pastorder in result.OrderHistory)
        {
            Console.WriteLine(pastorder.timeRecieved);
            Console.WriteLine(pastorder.timeFulfilled);
            foreach (IceCream pastorderice in pastorder.IceCreamlist)
            {
                Console.WriteLine(pastorder);

            }


        }
    }
    else { Console.WriteLine("invalid customer"); }
    


    // for each order, display all the details of the order including datetime received, datetime
    //fulfilled(if applicable) and all ice cream details associated with the order

}
void Option6()
    
{
    
    
    Option1(DictCustomer);
    Console.Write("Select the customer: ");
    int idInp = Convert.ToInt32(Console.ReadLine());
    customer? result = Search(DictCustomer, idInp);

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
            while (true)
            {
                Console.Write("Enter their ice cream order type (Cup, Cone or Waffle): ");
                string choiceInp = Console.ReadLine();
                Makeicecream(choiceInp, result);
                Console.Write("Do you wish to continue ordering? (Y/N): ");
                string yesorno = Console.ReadLine();
                if (yesorno.ToLower() == "y")
                {
                    continue;

                }
                else if (yesorno.ToLower() == "n")
                {
                    Console.WriteLine("order finished.");
                    break;

                }
                else
                {
                    Console.WriteLine("invalid input.order finished.");


                }
            }

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
    else {
        Console.WriteLine("invalid custoemr.");

    }
       

    

}
void Option7() { 
    Order servingorder = RegularQueueOrder.Dequeue();
    double total = servingorder.CalcualteTotal();
    foreach (IceCream ice in servingorder.IceCreamlist) {
        Console.WriteLine(ice);

    }
    Console.WriteLine($"total cost: {total}");
    customer servingcustomer = null;
    foreach(var custo in DictCustomer) {
        customer customers = custo.Value;
        if (customers != null)
        {
            if (customers.CurrentOrder.id == servingorder.id)
            {
                servingcustomer = customers;
                break;


            }
            else {
                continue;
            
            }
        }
    
    
    }
    Console.WriteLine($"membership teir: {servingcustomer.Rewards.tier}        points: {servingcustomer.Rewards.points}");
    if (servingcustomer.IsBirthday()) {
        Console.WriteLine("happy birthday");
        double highest = 0;
        foreach (IceCream ice in servingorder.IceCreamlist) {
            if (ice.CalculatePrice() >= highest)
            {
                highest = ice.CalculatePrice();

            }
            else {
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


    }
    else {
        Console.WriteLine($"points: {servingcustomer.Rewards.points}");
        Console.Write("would u like to redeem some points(y/n): ");
        string redeem = Console.ReadLine();
        if (redeem == "y") {
            Console.Write("how much point would u like to redeem(1 point = 0.02): ");
            int point = Convert.ToInt16(Console.ReadLine());
            double discounted = point * 0.02;
            total -= discounted;

        }
        else if (redeem == "n") { }
        int points = Convert.ToInt16(Math.Floor(total * 0.72));
        servingcustomer.Rewards.AddPoints(points);
        servingcustomer.CurrentOrder.timeFulfilled = DateTime.Now;
        servingcustomer.OrderHistory.Add(servingcustomer.CurrentOrder);
        servingcustomer.CurrentOrder = null ;

    }
    foreach (IceCream ice in servingorder.IceCreamlist) {
        servingcustomer.Rewards.Punch();
        if (servingcustomer.Rewards.punchCard == 10) {
            break;
        
        }
    
    }



} 
void Option8() {
    
        
    
}

Dictionary<int,string> wafflelist = new Dictionary<int,string>();
void initwaffle(Dictionary<int, string> wafflelist) {
    wafflelist.Add(1, "Original");
    wafflelist.Add(2, "Red velvet");
    wafflelist.Add(3, "charcoal");
    wafflelist.Add(4, "pandan waffle");




}
void displaywaffle(Dictionary<int, string> wafflelist) {
    foreach (var kpv in wafflelist) {
        Console.WriteLine($"[{kpv.Key}]: {kpv.Value,-10}");

    }

}




void Makeicecream(string type,customer result) {
    IceCream newIceCream = null;
    List<Flavour> flavlist = new List<Flavour>();
    List<Topping> toplist = new List<Topping>();
    Dictionary<int, string> wafflelist = new Dictionary<int, string>();
    initwaffle(wafflelist);

    if (type == "cup")
    {
        Console.Write("Enter number of scoops: ");


        int newscp = Convert.ToInt16(Console.ReadLine());

        DisplayFlavours(DictFlavour);
        for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
        {
            Console.Write($"Enter flavour number {scoopIndex} : ");
            int newflav = Convert.ToInt32(Console.ReadLine());
            Flavour addflact =DictFlavour[newflav ];
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
        for (int topIndex = 1; topIndex <= newtop; topIndex++) {
            Console.Write($"Enter toping number {topIndex} : ");
            int addtop = Convert.ToInt32(Console.ReadLine());
            Topping toppingtolist = DictTopping[addtop ];
            toplist.Add(toppingtolist);


        }
        newIceCream = new Cup("cup",newscp,flavlist,toplist);
        result.CurrentOrder.AddIceCream(newIceCream);
        




    }


    else if (type == "waffle") {
        Console.Write("Enter number of scoops: ");


        int newscp = Convert.ToInt16(Console.ReadLine());

        DisplayFlavours(DictFlavour);
        for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
        {
            Console.Write($"Enter flavour number {scoopIndex} : ");
            int newflav = Convert.ToInt32(Console.ReadLine());
            Flavour addflact = DictFlavour[newflav ];
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
        newIceCream = new Waffle("waffle", newscp, flavlist, toplist,waffeflav);
        result.CurrentOrder.AddIceCream(newIceCream);

    }
    else if (type == "con") {
        Console.Write("Enter number of scoops: ");


        int newscp = Convert.ToInt16(Console.ReadLine());

        DisplayFlavours(DictFlavour);
        for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
        {
            Console.Write($"Enter flavour number {scoopIndex} : ");
            int newflav = Convert.ToInt32(Console.ReadLine());
            Flavour addflact = DictFlavour[newflav ];
            if (addflact != null)
            {
                flavlist.Add(addflact);
            }
            else {
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
        if (dipped.ToLower() == "y") {
            newIceCream = new Cone("cone", newscp, flavlist, toplist, true);


        }
        else if (dipped.ToLower() == "n") {
            newIceCream = new Cone("cone", newscp, flavlist, toplist, false);


        }
        
        result.CurrentOrder.AddIceCream(newIceCream);

    }



    
}
void modifydiplay() {
    Console.WriteLine("[1] change type");
    Console.WriteLine("[2] cahnge number of scoop");
    Console.WriteLine("[3] cahnge flavour of scoop");
    Console.WriteLine("[4] change toppings");
}


void modifyicecream(IceCream result,customer cust ) {
    Dictionary<int, string> wafflelist = new Dictionary<int, string>();
    initwaffle(wafflelist);
    List<Flavour> flavlist = new List<Flavour>();
    IceCream modifiedice = null;
    if (result is Cone)
    {
        modifydiplay();
        Console.WriteLine("[5] cone dipped option");
        Console.Write("Enter an option: ");
        string option = Console.ReadLine();
        if (option == "1")
        {
            Console.Write("Enter new type(Cone or cup): ");
            string newtype = Console.ReadLine();
            if (newtype == "cup")
            {
                modifiedice = new Cup("cup", result.Scoops, result.Flavours, result.Toppings);

            }
            else if (newtype == "waffle")
            {
                displaywaffle(wafflelist);
                Console.Write("Enter waffle flavour: ");
                int wafflenum = Convert.ToInt32(Console.ReadLine());
                string waffeflav = wafflelist[wafflenum];
                modifiedice = new Waffle("waffle", result.Scoops, result.Flavours, result.Toppings, waffeflav);
            }
            cust.CurrentOrder.IceCreamlist.Remove(result);
            cust.CurrentOrder.AddIceCream(modifiedice);


        }
        else if (option == "2")
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
            result.Flavours = flavlist;
        }
        else if (option == "3")
        {
            flavlist.Clear();
            int count = 1;
            foreach (var flavour in result.Flavours)
            {
                Console.Write($"Enter flavour number {count} : ");
                count++;
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
            result.Flavours = flavlist;

        }
        else if (option == "4")
        {
            List<Topping> toplist = new List<Topping>();
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
            result.Toppings = toplist;
        }
        else if (option == "5") {
            Console.Write("Do you want your cone dipped?(Y/N):");
            string dipped = Console.ReadLine();
            if (dipped.ToLower() == "y")
            {
                modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, true);



            }
            else if (dipped.ToLower() == "n")
            {
                modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, false);



            }
            cust.CurrentOrder.IceCreamlist.Remove(result);
            cust.CurrentOrder.AddIceCream(modifiedice);

        }


    }
    else if (result is Waffle)
    {
        modifydiplay();
        Console.WriteLine("[5] change waffle flavour");
        Console.Write("Enter an option: ");
        string option = Console.ReadLine();
        if (option == "1")
        {
            Console.Write("Enter new type(Cone or cup): ");
            string newtype = Console.ReadLine();
            if (newtype == "cone")
            {
                Console.Write("Do you want your cone dipped?(Y/N):");
                string dipped = Console.ReadLine();
                if (dipped.ToLower() == "y")
                {
                    modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, true);



                }
                else if (dipped.ToLower() == "n")
                {
                    modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, false);



                }
            }
            else if (newtype == "cup")
            {
                modifiedice = new Cup("cup", result.Scoops, result.Flavours, result.Toppings);

            }
            cust.CurrentOrder.IceCreamlist.Remove(result);
            cust.CurrentOrder.AddIceCream(modifiedice);
        }
        else if (option == "2")
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
            result.Flavours = flavlist;
        }
        else if (option == "3")
        {
            flavlist.Clear();
            int count = 1;
            foreach (var flavour in result.Flavours)
            {
                Console.Write($"Enter flavour number {count} : ");
                count++;
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
            result.Flavours = flavlist;

        }
        else if (option == "4")
        {
            List<Topping> toplist = new List<Topping>();
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
            result.Toppings = toplist;
        }
        else if (option == "5")
        {
            displaywaffle(wafflelist);
            Console.Write("Enter waffle flavour: ");
            int wafflenum = Convert.ToInt32(Console.ReadLine());
            string waffeflav = wafflelist[wafflenum];
            modifiedice = new Waffle("waffle", result.Scoops, result.Flavours, result.Toppings, waffeflav);
            cust.CurrentOrder.IceCreamlist.Remove(result);
            cust.CurrentOrder.AddIceCream(modifiedice);

        }

    }
    else if (result is Cup)
    {
        modifydiplay();
        Console.Write("Enter an option: ");
        string option = Console.ReadLine();
        if (option == "1")
        {
            Console.Write("Enter new type(Cone or Waffle): ");
            string newtype = Console.ReadLine();
            if (newtype == "cone")
            {
                Console.Write("Do you want your cone dipped?(Y/N):");
                string dipped = Console.ReadLine();
                if (dipped.ToLower() == "y")
                {
                    modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, true);



                }
                else if (dipped.ToLower() == "n")
                {
                    modifiedice = new Cone("cone", result.Scoops, result.Flavours, result.Toppings, false);



                }
            }
            else if (newtype == "waffle")
            {
                displaywaffle(wafflelist);
                Console.Write("Enter waffle flavour: ");
                int wafflenum = Convert.ToInt32(Console.ReadLine());
                string waffeflav = wafflelist[wafflenum];
                modifiedice = new Waffle("waffle", result.Scoops, result.Flavours, result.Toppings, waffeflav);
            }
            cust.CurrentOrder.IceCreamlist.Remove(result);
            cust.CurrentOrder.AddIceCream(modifiedice);


        }
        else if (option == "2")
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
            result.Flavours = flavlist;
        }
        else if (option == "3")
        {
            flavlist.Clear();
            int count = 1;
            foreach (var flavour in result.Flavours)
            {
                Console.Write($"Enter flavour number {count} : ");
                count++;
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
            result.Flavours = flavlist;

        }
        else if (option == "4")
        {
            List<Topping> toplist = new List<Topping>();
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
            result.Toppings = toplist;
        }


    }

}

void orderscsv()
{
    using (StreamReader sr = new StreamReader("orders.csv"))
    {
        string? header = sr.ReadLine();
        if (header == null)
        {
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                if (line != null)
                {
                    
                    string[] stringtolist = line.Split(",");
                    if (stringtolist != null) {
                        customer custom = DictCustomer[Convert.ToInt16(stringtolist[1])];
                        Order newor = null;
                        IceCream iceCream = null;
                        if (custom.OrderHistory == null)
                        {
                            newor = new Order(Convert.ToInt16(stringtolist[0]), DateTime.Parse(stringtolist[3]));
                            newor.timeFulfilled = DateTime.Parse(stringtolist[4]);

                        }
                        else
                        {
                            foreach (Order or in custom.OrderHistory)
                            {
                                if (or.id == Convert.ToInt16(stringtolist[0]))
                                {
                                    newor = or;

                                }

                            }
                        }




                        if (stringtolist[4].ToLower() == "cup")
                        {
                            List<Flavour> fkav = new List<Flavour>();




                            int scoops = Convert.ToInt16(stringtolist[5]);


                            for (int i = 0; i <= scoops; i++)
                            {
                                Flavour flavour = new Flavour(stringtolist[7 + i], true,1);
                                fkav.Add(flavour);

                            }
                            List<Topping> topping = new List<Topping>();
                            for (int i = 0; i <= 4; i++)
                            {
                                string newtop = stringtolist[11 + i];
                                if (newtop != null)
                                {
                                    Topping tops = new Topping(newtop);
                                    topping.Add(tops);


                                }
                                else
                                {
                                    break;

                                }



                            }
                            iceCream = new Cone("cone", scoops, fkav, topping, Convert.ToBoolean(stringtolist[6]));


                        }
                        else if (stringtolist[4].ToLower() == "waffle")
                        {
                            List<Flavour> fkav = new List<Flavour>();




                            int scoops = Convert.ToInt16(stringtolist[5]);


                            for (int i = 0; i <= scoops; i++)
                            {
                                Flavour flavour = new Flavour(stringtolist[7 + i], true,1);
                                fkav.Add(flavour);

                            }
                            List<Topping> topping = new List<Topping>();
                            for (int i = 0; i <= 4; i++)
                            {
                                string newtop = stringtolist[11 + i];
                                if (newtop != null)
                                {
                                    Topping tops = new Topping(newtop);
                                    topping.Add(tops);


                                }
                                else
                                {
                                    break;

                                }



                            }


                            iceCream = new Waffle("waffle", scoops, fkav, topping, stringtolist[7]);
                        }

                        else if (stringtolist[4].ToLower() == "cup")
                        {
                            List<Flavour> fkav = new List<Flavour>();




                            int scoops = Convert.ToInt16(stringtolist[5]);


                            for (int i = 0; i <= scoops; i++)
                            {
                                Flavour flavour = new Flavour(stringtolist[7 + i], true,1);
                                fkav.Add(flavour);

                            }
                            List<Topping> topping = new List<Topping>();
                            for (int i = 0; i <= 4; i++)
                            {
                                string newtop = stringtolist[11 + i];
                                if (newtop != null)
                                {
                                    Topping tops = new Topping(newtop);
                                    topping.Add(tops);


                                }
                                else
                                {
                                    break;

                                }



                            }
                            iceCream = new Cup("cone", scoops, fkav, topping);

                        }
                        





                       
                        newor.IceCreamlist.Add(iceCream);

                    }

                }

            }






        }


    }


}