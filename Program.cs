using PRG2_Assignment;
using PRG2_Assignment_Flavour;
using PRG2_Assignment_IceCream;
using PRG2_Assignment_Topping;
using System;

void Option1() { }
void Option2() { }
void Option3() {


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
    else if (choice == 2) {
        List<Flavour> flavlist = new List<Flavour>();
        List<Topping> toplist = new List<Topping>();
        Console.WriteLine("Enter optionn : ");
        string newopt = Console.ReadLine();
        
        Console.WriteLine("Enter number of scoops: ");
        

        int newscp = Convert.ToInt16(Console.ReadLine());
        for (int scoopIndex = 1; scoopIndex <= newscp; scoopIndex++)
        {
            Console.WriteLine($"Enter flavour number {scoopIndex} : ");
            string newflav = Console.ReadLine();
            if (newflav.ToLower() == "durian" || newflav.ToLower() == "ube" || newflav.ToLower() == "sea slat")
            {
                foreach (Flavour flav in flavlist) {
                    if (flav.Type == newflav.ToLower())
                    {
                        flav.Quantity += 1;


                    }
                    else {
                        Flavour flavour = new Flavour(newflav, true, 1);
                        flavlist.Add(flavour);

                    }
                
                }
                


            }
            else {

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

        for (int topIndex = 1; topIndex <= newtop; topIndex++) {
            Console.WriteLine($"Enter topping number {1} : ");
            string top = Console.ReadLine();
            Topping tops = new Topping(top);
            toplist.Add(tops);



        }
        IceCream newice = new IceCream(newopt,newscp,flavlist,toplist);


    }
    else if (choice == 3) { }

}

