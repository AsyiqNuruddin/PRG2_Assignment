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
    Console.Write("Enter number of toppings: ");
    int topCount = Convert.ToInt32(Console.ReadLine());
    List<Topping> topList = new List<Topping>();
    if (topCount > 0)
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
        }else if (wafInp == "plain")
        {
            return "Plain";
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
                Console.Write("Do you want a waffle flavour?(Red velvet, charcoal, or pandan) or plain: ");
                string wafInp = Console.ReadLine();
                string waf = WaffleChoice(wafInp);
                if (waf == "Red Velvet")
                {
                    (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                    newice = new Waffle("Waffle", cat.Item1, cat.Item2, cat.Item3, waf);
                }
                else if (waf == "Charcoal")
                {
                    (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                    newice = new Waffle("Waffle", cat.Item1, cat.Item2, cat.Item3, waf);
                }
                else if (waf == "Pandan")
                {
                    (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                    newice = new Waffle("Waffle", cat.Item1, cat.Item2, cat.Item3, waf);
                }
                else if(waf == "Plain")
                {
                    {
                        (int, List<Flavour>, List<Topping>) cat = IceCreamAdd(DictFlavour, DictTopping);
                        newice = new Waffle("Waffle", cat.Item1, cat.Item2, cat.Item3, waf);
                    }
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
<<<<<<< HEAD
        if(result.CurrentOrder.IceCreamlist.Count != 0)
=======
        if (result.CurrentOrder.IceCreamlist.Count == null)
>>>>>>> 27e0ab3605b641784a43009bf13c88771645fe38
        {
            Console.WriteLine($"\nOrder Number[{result.CurrentOrder.id}] is successfull");
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
        
        
    }
    else
    {
        Console.WriteLine("Customer not found");
    }

}
void Option5() {
    Option1(DictCustomer);
    Console.Write("Select the customer: ");
    int idInp = Convert.ToInt32(Console.ReadLine());
    customer? result = Search(DictCustomer, idInp);
    List<IceCream> currentorder = result.CurrentOrder.IceCreamlist;
    Console.WriteLine("current order");
    Console.WriteLine(result.CurrentOrder.timeRecieved);
    foreach (IceCream currenrorderice in currentorder) {
        Console.WriteLine(currenrorderice);


    }
    Console.WriteLine("pass orders");
    foreach (Order pastorder in result.OrderHistory) {
        Console.WriteLine(pastorder.timeRecieved);
        Console.WriteLine(pastorder.timeFulfilled);
        foreach (IceCream pastorderice in pastorder.IceCreamlist) {
            Console.WriteLine(pastorder);

        }
    
    
    } 
    


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
        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice == 1)
        {
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

            modifyicecream(Modifyice, result);

        }
        else if (choice == 2)
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
                    break;

                }
            }

        }
        else if (choice == 3) {
            int count = 1;
            foreach (IceCream or in result.CurrentOrder.IceCreamlist)
            {
                Console.WriteLine($"[{count}]");
                Console.WriteLine(or);
                count++;


            }
            Console.Write("Enter a ice cream to remove: ");
            int icecreanindex = Convert.ToInt32(Console.ReadLine());
            IceCream Modifyice = result.CurrentOrder.IceCreamlist[icecreanindex - 1];
            result.CurrentOrder.DeleteIceCream(Modifyice);



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



