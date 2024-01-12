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
Queue<Order> GoldQueueOrder = new Queue<Order>();
Queue<Order> RegularQueueOrder = new Queue<Order>();
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
    }else if (usrInp == "7")
    {
        //Option7();
    }else if (usrInp == "8")
    {
        //Option8();
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
            customer newCustomer = new customer(rowList[0], Convert.ToInt32(rowList[1]), Convert.ToDateTime(rowList[2]));
            newCustomer.Rewards = new PointCard();
            DictCustomer.Add(Convert.ToInt32(rowList[1]), newCustomer);
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
        Console.WriteLine($"Name: {kvp.Value.Name,-15} Member ID:{kvp.Value.MemberId,-10} DateofBirth: {kvp.Value.Dob,-10:dd/MM/yyyy}");
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
    customer newCustomer = new customer(nameInp,idInp,dob);
    Console.WriteLine("Their registration customer details");
    Console.WriteLine($"Name: {newCustomer.Name,-10} Member ID:{newCustomer.MemberId,-10} DateofBirth: {newCustomer.Dob,-10:dd/MM/yyyy}");
    PointCard newPC = new PointCard(0,0);
    newCustomer.Rewards = newPC;

    using (StreamWriter sw = new StreamWriter("customers.csv", true))
    {
        string? row;
        row = string.Join(",", newCustomer.Name, newCustomer.MemberId, $"{newCustomer.Dob:dd/MM/yyyy}");
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
    for (int i = 1;i < topCount + 1;i++)
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
    IceCream newice = null;
    Console.Write("Select the customer: ");
    int idInp = Convert.ToInt32(Console.ReadLine());
    customer? result = Search(DictCustomer, idInp);
    if (result != null)
    {
        Console.WriteLine("Found Customer ");
        Order newOrd = result.CurrentOrder;
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
                Console.Write("Do you want your cone dipped?(Red velvet, charcoal, or pandan): ");
                string wafInp = Console.ReadLine();
                string waf = WaffleChoice(wafInp);
                if (waf == "red velvet")
                {
                    (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                    newice = new Waffle("Waffle", cat.Item1, cat.Item2, cat.Item3, wafInp);
                }
                else if (wafInp == "charcoal")
                {
                    (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                    newice = new Waffle("Waffle", cat.Item1, cat.Item2, cat.Item3, wafInp);
                }
                else if (wafInp == "pandan")
                {
                    (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                    newice = new Waffle("Waffle", cat.Item1, cat.Item2, cat.Item3, wafInp);
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
                Console.WriteLine("");
            }
        }
        if (result.CurrentOrder.IceCreamlist.Count == null)
        {
            result.MakeOrder();
            Console.WriteLine($"Order [{result.OrderHistory.Last().id}] is successfull");
            string printed = $"Total Number of Ice Creams: {newOrd.IceCreamlist.Count}\n";
            foreach (var v in newOrd.IceCreamlist)
            {
                printed += $"{v}\n";
            }
            Console.WriteLine(printed);
            if (result.Rewards.tier == "gold")
            {
                GoldQueueOrder.Enqueue(newOrd);
            }
            else
            {
                RegularQueueOrder.Enqueue(newOrd);
            }

        }
        else { 
            result.CurrentOrder.AddIceCream(newice);
        
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
void Option6()
{
    InitCustomer("customers.csv");
    Option1(DictCustomer);
    Console.Write("Select the customer: ");
    int idInp = Convert.ToInt32(Console.ReadLine());
    customer? result = Search(DictCustomer, idInp);
    Console.WriteLine(result);
    if (result != null)
    {
        Console.WriteLine("Menu:\r\n1. Modify an existing ice cream in the order\r\n2. Add a new ice cream to the order\r\n3. Delete an existing ice cream from the order");
        Console.Write("Please enter the number corresponding to your choice: ");
        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice == 1) {
            int count = 1;
            foreach (IceCream or in result.CurrentOrder.IceCreamlist)
            {
                Console.WriteLine($"[{count}]");
                Console.WriteLine(or);
                count++;


            }
            Console.Write("Enter a ice cream to modify: ");
            int icecreanindex = Convert.ToInt32(Console.ReadLine());
            IceCream Modifyice = result.CurrentOrder.IceCreamlist[icecreanindex - 1];
            Console.Write("Enter their ice cream order type (Cup, Cone or Waffle): ");
            string choiceInp = Console.ReadLine();

        }
        else if (choice == 2) {
            while (true) {
                Console.Write("Enter their ice cream order type (Cup, Cone or Waffle): ");
                string choiceInp = Console.ReadLine();
                Makeicecream(choiceInp,result);
                Console.Write("Do you wish to continue ordering? (Y/N): ");
                string yesorno = Console.ReadLine();
                if (yesorno.ToLower() == "y")
                {
                    continue;

                }
                else if (yesorno.ToLower() == "n") {
                    break;
                
                }
            }

        }
    }
       

    

}
Dictionary<int,string> wafflelist = new Dictionary<int,string>();
void initwaffle(Dictionary<int, string> wafflelist) {
    wafflelist.Add(1, "regular");
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
            flavlist.Add(addflact);



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
            flavlist.Add(addflact);



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
    else if (type == "coon") {
        Console.Write("Enter number of scoops: ");


        int newscp = Convert.ToInt16(Console.ReadLine());

        DisplayFlavours(DictFlavour);
        for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
        {
            Console.Write($"Enter flavour number {scoopIndex} : ");
            int newflav = Convert.ToInt32(Console.ReadLine());
            Flavour addflact = DictFlavour[newflav ];
            flavlist.Add(addflact);



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
void modifyicecream(string type,customer result ) { 

}



