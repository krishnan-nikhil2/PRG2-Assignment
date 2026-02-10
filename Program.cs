//==========================================================
// Student Number : S10272831G, S10272956J
// Student Names : Krishnan Nikhil, Li Yanjie James
//==========================================================

// 2) Load customers and orders
using Git_Prg2_Assignment;
using System.Collections.Generic;
using System.Xml.Serialization;

List<Restaurant> Restaurant_List = new List<Restaurant>();
List<Customer> Customer_List = new List<Customer>();
List<FoodItem> FoodItem_List = new List<FoodItem>();
List<Order> Order_List = new List<Order>();


void LoadRestaurantsAndFoodItems()
{
    using (StreamReader sr = new StreamReader("restaurants.csv"))
    {

        string line;
        line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            string[] Restaurant_attributes = line.Split(",");
            Restaurant restaurant = new Restaurant(Restaurant_attributes[0], Restaurant_attributes[1], Restaurant_attributes[2]);
            Restaurant_List.Add(restaurant);
        }
    }


    using (StreamReader sr = new StreamReader("fooditems.csv"))
    {
        string line;
        line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {

            string[] fooditem_attributes = line.Split(",");
            FoodItem fooditem = new FoodItem(fooditem_attributes[1], fooditem_attributes[2], Convert.ToDouble(fooditem_attributes[3]), "");
            FoodItem_List.Add(fooditem);
            foreach (Restaurant res in Restaurant_List)
            {
                if (fooditem_attributes[0] == res.RestaurantId)
                {
                    res.Menu_List[0].AddFoodItem(fooditem);
                }
            }
        }
    }


}


void LoadCustomersAndOrders()
{
    using (StreamReader sr = new StreamReader("customers.csv"))
    {
        string line;
        line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            string[] customer_attributes = line.Split(",");
            Customer customer = new Customer(customer_attributes[1], customer_attributes[0]);
            Customer_List.Add(customer);
        }
    }





    using (StreamReader sr = new StreamReader("orders.csv"))
    {
        string line;
        line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            string[] order_details = line.Split('"');
            string[] order_attributes = order_details[0].Split(",");
            string[] deliverydate_attributes = order_attributes[3].Split("/");
            string[] deliverytime_attributes = order_attributes[4].Split(":");
            string[] orderdate_time = order_attributes[6].Split(" ");
            string[] orderdate_attributes = orderdate_time[0].Split("/");
            string[] ordertime_attributes = orderdate_time[1].Split(":");

            string[] orderedfooditemlist = order_details[1].Split("|");

            DateTime deliverydatetime = new DateTime(Convert.ToInt32(deliverydate_attributes[2]), Convert.ToInt32(deliverydate_attributes[1]), Convert.ToInt32(deliverydate_attributes[0]), Convert.ToInt32(deliverytime_attributes[0]), Convert.ToInt32(deliverytime_attributes[1]), DateTime.Now.Second);
            DateTime orderdatetime = new DateTime(Convert.ToInt32(orderdate_attributes[2]), Convert.ToInt32(orderdate_attributes[1]), Convert.ToInt32(orderdate_attributes[0]), Convert.ToInt32(ordertime_attributes[0]), Convert.ToInt32(ordertime_attributes[1]), DateTime.Now.Second);
            Order order = new Order(Convert.ToInt32(order_attributes[0]), orderdatetime, Convert.ToDouble(order_attributes[7]), order_attributes[8], deliverydatetime, order_attributes[5], "", false);


            foreach (string orderedfooditemstring in orderedfooditemlist)
            {
                string[] orderfooditem_attributes = orderedfooditemstring.Split(",");

                foreach (FoodItem foodItem in FoodItem_List)
                {
                    if (foodItem.ItemName == orderfooditem_attributes[0])
                    {
                        OrderedFoodItem orderedfooditem = new OrderedFoodItem(orderfooditem_attributes[0], foodItem.ItemDesc, foodItem.ItemPrice, foodItem.Customise, Convert.ToInt32(orderfooditem_attributes[1]), 0.0);
                        order.AddOrderedFoodItem(orderedfooditem);
                    }
                }

            }

            Order_List.Add(order);

            foreach (Restaurant restaurant in Restaurant_List)
            {
                if (restaurant.RestaurantId == order_attributes[2])
                {
                    order.restaurant = restaurant;
                    restaurant.Order_Queue.Enqueue(order);
                }
            }


            foreach (Customer customer in Customer_List)
            {
                if (customer.EmailAddress == order_attributes[1])
                {
                    order.Customer = customer;
                    customer.AddOrder(order);
                }
            }

        }

    }


}


string Display_Gruberoo_Food_deliver_System_Menu()
{
    Console.WriteLine("===== Gruberoo Food Delivery System =====");
    Console.WriteLine("1. List all restaurants and menu items");
    Console.WriteLine("2. List all orders");
    Console.WriteLine("3. Create a new order");
    Console.WriteLine("4. Process an order");
    Console.WriteLine("5. Modify an existing order");
    Console.WriteLine("6. Delete an existing order");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");
    string choice = Console.ReadLine();
    return choice;
}

void Display_Restaurants_and_Menu_Items()
{
    Console.WriteLine();
    Console.WriteLine("All Restaurants and Menu Items");
    Console.WriteLine("==============================");
    foreach (Restaurant restaurant in Restaurant_List)
    {
        restaurant.DisplayMenu();
        Console.WriteLine();
    }
    Console.WriteLine();

}


void ListAllOrders()
{
    Console.WriteLine();
    Console.WriteLine("All Orders");
    Console.WriteLine("==========");
    Console.WriteLine($"{"Order ID",-10} {"Customer",-15} {"Restaurant",-20} {"Delivery Date/Time",-25} {"Amount",-10} {"Status",-10}");
    Console.WriteLine($"{"--------",-10} {"----------",-15} {"-------------",-20} {"------------------",-25} {"------",-10} {"---------",-10}");
    foreach (Order order in Order_List)
    {
        Console.WriteLine($"{order.OrderId,-10} {order.Customer.CustomerName,-15} {order.restaurant.RestaurantName,-20} {order.DeliveryDateTime.ToString("dd/MM/yyyy HH:mm"),-25} ${order.CalculateOrderTotal().ToString("0.00"),-10} {order.OrderStatus,-10}");
    }
    Console.WriteLine();
}



void CreateOrder()
{

    Restaurant res = new Restaurant();

    Console.WriteLine();
    Console.WriteLine("Create New Order");
    Console.WriteLine("================");

    string customer_email = "";
    string restuarant_id = "";
    string string_delivery_date = "";
    string string_delivery_time = "";
    DateTime delivery_date = new DateTime();
    DateTime delivery_time = new DateTime();
    string items_list = "";
    string items_and_qty = "";
    Order order = new Order();
    OrderedFoodItem orderedFoodItem = new OrderedFoodItem();
    double total = 0;
    string payment_method = "";
    string delivery_address = "";


    while (true)
    {
        int c = 0;
        Console.Write("Enter Customer Email: ");
        customer_email = Console.ReadLine();
        foreach (Customer customer in Customer_List)
        {
            if (customer_email == customer.EmailAddress)
            {
                c += 1;
                break;
            }
        }
        if (c == 0)
        {
            Console.WriteLine("Please enter a valid email address");
        }
        else
        {
            break;
        }


    }


    while (true)
    {
        int c = 0;
        Console.Write("Enter Restaurant ID: ");
        restuarant_id = Console.ReadLine();

        foreach (Restaurant restaurant in Restaurant_List)
        {
            if (restuarant_id == restaurant.RestaurantId)
            {
                c += 1;
                res = restaurant;
                break;
            }
        }
        if (c == 0)
        {
            Console.WriteLine("Please enter a valid restaurant id");
        }
        else
        {
            break;
        }

    }


    while (true)
    {
        try
        {
            Console.Write("Enter Delivery Date (dd/mm/yyyy): ");
            string_delivery_date = Console.ReadLine();
            if (string_delivery_date.Split("/").Length != 3)
            {
                Console.WriteLine("Please enter the date with slash format");
                continue;
            }
            delivery_date = Convert.ToDateTime(string_delivery_date);
            break;
        }

        catch (FormatException ex)
        {
            Console.WriteLine("Please enter the delivery date in the correct format with numbers");
        }



    }

    while (true)
    {
        try
        {
            Console.Write("Enter Delivery Time (hh:mm): ");
            string_delivery_time = Console.ReadLine();
            delivery_time = Convert.ToDateTime(string_delivery_time);
            break;
        }


        catch (FormatException ex)
        {
            Console.WriteLine("Please enter the delivery time in the correct format");
        }
    }


    while (true)
    {
        Console.Write("Enter Delivery Address: ");
        delivery_address = Console.ReadLine();
        if (delivery_address != "")
        {
            break;
        }

        else
        {
            Console.WriteLine("Address cannot be empty");
        }


    }

    Console.WriteLine();
    Console.WriteLine("Available Food Items:");


    int count = 1;

    foreach (Menu menu in res.Menu_List)
    {
        foreach (FoodItem fooditem in menu.FoodItem_List)
        {
            Console.WriteLine($"{count,-3}. {fooditem.ItemName,-10} - ${fooditem.ItemPrice.ToString("0.00")}");
            count += 1;
        }
        count = 1;
    }


    count = res.Menu_List[0].FoodItem_List.Count;

    while (true)
    {
        try
        {
            Console.Write("Enter item number (0 to finish): ");
            int option = Convert.ToInt32(Console.ReadLine());
            if (option >= 1 && option <= count)
            {
                while (true)
                {
                    try
                    {

                        Console.Write("Enter quantity: ");
                        int qty = Convert.ToInt32(Console.ReadLine());
                        items_and_qty = $"{res.Menu_List[0].FoodItem_List[option - 1].ItemName}, {qty}";
                        items_list = items_list + items_and_qty + "|";

                        Console.Write("Add special request? [Y/N]: ");
                        string choice = Console.ReadLine();
                        string special_request = "";

                        if (choice == "Y")
                        {
                            Console.Write("Enter your special request: ");
                            special_request = Console.ReadLine();
                        }



                        for (int i = 0; i < res.Menu_List[0].FoodItem_List.Count; i++)
                        {
                            if (i == (option - 1))
                            {
                                orderedFoodItem = new OrderedFoodItem();
                                FoodItem item = res.Menu_List[0].FoodItem_List[i];
                                orderedFoodItem.ItemName = item.ItemName;
                                orderedFoodItem.ItemDesc = item.ItemDesc;
                                orderedFoodItem.ItemPrice = item.ItemPrice;
                                orderedFoodItem.Customise = special_request;
                            }
                        }

                        orderedFoodItem.QtyOrdered = qty;
                        orderedFoodItem.SubTotal = orderedFoodItem.CalculateSubTotal();

                        total += orderedFoodItem.SubTotal;

                        order.AddOrderedFoodItem(orderedFoodItem);

                        break;
                    }

                    catch (FormatException ex)
                    {
                        Console.WriteLine("PLease enter a number for the quantity");
                    }
                }


            }
            else if (option == 0)
            {

                Console.WriteLine();
                Console.WriteLine($"Order Total: ${total} + $5.00 (delivery) = ${total + 5}");
                string payment_option = "";

                while (true)
                {
                    Console.Write("Proceed to payment? [Y/N]: ");

                    payment_option = Console.ReadLine();

                    if (payment_option != "N" && payment_option != "Y")
                    {
                        Console.WriteLine("Please Enter a valid option");
                    }
                    else
                    {
                        break;
                    }

                }

                if (payment_option == "Y")
                {

                    Console.WriteLine();


                    while (true)
                    {
                        Console.WriteLine("Payment method:");
                        Console.Write("[CC] Credit Card / [PP] PayPal / [CD] Cash on Delivery: ");
                        payment_method = Console.ReadLine();
                        if (payment_method != "CC" && payment_method != "CD" && payment_method != "PP")
                        {
                            Console.WriteLine("Please enter a valid payment method");
                        }
                        else
                        {
                            break;
                        }
                    }

                    Console.WriteLine();

                    DateTime order_datetime = DateTime.Now;


                    string[] deliverydate_attributes = string_delivery_date.Split("/");
                    string[] deliverytime_attributes = string_delivery_time.Split(":");

                    DateTime delivery_datetime = new DateTime(Convert.ToInt32(deliverydate_attributes[2]), Convert.ToInt32(deliverydate_attributes[1]), Convert.ToInt32(deliverydate_attributes[0]), Convert.ToInt32(deliverytime_attributes[0]), Convert.ToInt32(deliverytime_attributes[1]), DateTime.Now.Second);


                    order.OrderId = Order_List[Order_List.Count - 1].OrderId + 1;
                    order.OrderDateTime = order_datetime;
                    order.OrderTotal = (order.CalculateOrderTotal() + 5.0);
                    order.OrderStatus = "Pending";
                    order.DeliveryDateTime = delivery_datetime;
                    order.DeliveryAddress = delivery_address;
                    order.OrderPaymentMethod = payment_method;
                    order.OrderPaid = true;

                    Console.WriteLine($"Order {order.OrderId} created successfully! Status: Pending");
                    Console.WriteLine();

                    foreach (Restaurant restaurant in Restaurant_List)
                    {
                        if (restaurant.RestaurantId == restuarant_id)
                        {
                            order.restaurant = restaurant;
                            restaurant.Order_Queue.Enqueue(order);
                            break;
                        }
                    }

                    foreach (Customer customer in Customer_List)
                    {
                        if (customer.EmailAddress == customer_email)
                        {
                            order.Customer = customer;
                            customer.AddOrder(order);

                            break;
                        }
                    }

                    Order_List.Add(order);

                    items_list.Remove(items_list.Length - 1);

                    using (StreamWriter sw = new StreamWriter("orders.csv", true))
                    {
                        sw.WriteLine($"{order.OrderId},{order.Customer.EmailAddress},{order.restaurant.RestaurantId},{string_delivery_date},{string_delivery_time},{delivery_address},{order.OrderDateTime},{order.OrderTotal},{order.OrderStatus},\"{items_list}\"");
                    }


                }





                break;
            }

        }

        catch (FormatException ex)
        {
            Console.WriteLine("Please only enter a  number for the item number");
        }
    }


}



void ModifyOrder()
{
    string customer_email;
    int orderid;
    Console.WriteLine();
    Console.WriteLine("Modify Order");
    Console.WriteLine("============");
    int customer_index = 0;
    int order_index = 0;
    int count = 0;
    List<int> orderid_list = new List<int>();
    Order Order = new Order();
    string address = "";
    int itemnumber = 0;
    int qty = 0;
    string payment_option = "";

    while (true)
    {
        int c = 0;
        Console.Write("Enter Customer Email: ");
        customer_email = Console.ReadLine();
        foreach (Customer customer in Customer_List)
        {
            if (customer_email == customer.EmailAddress)
            {
                c += 1;
                break;
            }
        }
        if (c == 0)
        {
            Console.WriteLine("Please enter a valid email address");
        }
        else
        {
            break;
        }


    }



    for (int i = 0; i < Customer_List.Count; i++)
    {
        if (Customer_List[i].EmailAddress == customer_email)
        {
            customer_index = i;
            break;
        }
    }

    Console.WriteLine("Pending Orders:");

    foreach (Order order in Customer_List[customer_index].Order_List)
    {
        if (order.OrderStatus == "Pending")
        {
            orderid_list.Add(order.OrderId);
            Console.WriteLine(order.OrderId);
            count += 1;
        }

    }



    if (count <= 0)
    {
        Console.WriteLine("You have no pending orders to modify");
    }
    else
    {
        while (true)
        {
            try
            {
                Console.Write("Enter Order ID: ");
                orderid = Convert.ToInt32(Console.ReadLine());
                if (orderid_list.Contains(orderid) == false)
                {
                    Console.WriteLine("Please enter a correct pending order id");
                    continue;
                }
                else
                {


                    for (int i = 0; i < Customer_List[customer_index].Order_List.Count; i++)
                    {
                        if (Customer_List[customer_index].Order_List[i].OrderId == orderid)
                        {
                            order_index = i;
                            break;
                        }
                    }

                    Console.WriteLine("Order Items:");

                    for (int j = 0; j < Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List.Count; j++)
                    {

                        Console.WriteLine($"{j + 1 + ".",-2} {Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[j].ItemName,-11} - {Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[j].QtyOrdered}");
                    }



                    Console.WriteLine("Address:");
                    Console.WriteLine(Customer_List[customer_index].Order_List[order_index].DeliveryAddress);

                    Console.WriteLine("Delivery Date/Time:");
                    Console.WriteLine($"{Customer_List[customer_index].Order_List[order_index].DeliveryDateTime.ToString("dd/MM/yyyy, HH:mm")}");


                    break;



                }
            }

            catch (FormatException ex)
            {
                Console.WriteLine("Please enter numbers only");
            }


        }
    }






    Console.WriteLine();




    while (true)
    {
        try
        {
            Console.Write("Modify: [1] Items [2] Address [3] Delivery Time: ");
            int option = Convert.ToInt32(Console.ReadLine());




            if (option == 1)
            {



                while (true)
                {
                    try
                    {
                        Console.Write("Enter the item number that you want to modify (0 to finish): ");
                        itemnumber = Convert.ToInt32(Console.ReadLine());

                        if (itemnumber < 0 || itemnumber > Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List.Count)
                        {
                            Console.WriteLine("Please only enter a valid item number");
                            continue;
                        }

                        break;
                    }

                    catch (FormatException ex)
                    {
                        Console.WriteLine("Please only enter a number");
                    }
                }


                while (true)
                {
                    try
                    {
                        Console.Write("Enter new quantity for ordered item: ");
                        qty = Convert.ToInt32(Console.ReadLine());
                        if (qty < 0)
                        {
                            Console.WriteLine("PLease enter positive number for the quantity");
                            continue;
                        }


                        break;

                    }

                    catch (FormatException ex)
                    {
                        Console.WriteLine("Please only enter a number");
                    }
                }

                if (qty > Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].QtyOrdered)
                {
                    Console.WriteLine($"Additional payment required: ${(qty - Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].QtyOrdered) * Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].ItemPrice}");


                    while (true)
                    {
                        Console.Write("Proceed to payment? [Y/N]: ");

                        payment_option = Console.ReadLine();

                        if (payment_option != "N" && payment_option != "Y")
                        {
                            Console.WriteLine("Please Enter a valid option");
                        }
                        else
                        {
                            break;
                        }

                    }

                    if (payment_option == "Y")
                    {
                        Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].QtyOrdered = qty;
                        Console.WriteLine();
                        Console.WriteLine($"Order {Customer_List[customer_index].Order_List[order_index].OrderId} updated. Changed {Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].ItemName} to {qty}");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                    }

                }

                else if (qty < Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].QtyOrdered)
                {
                    Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].QtyOrdered = qty;
                    Console.WriteLine($"Refund amount: {(Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].QtyOrdered - qty) * Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].ItemPrice}");
                    Console.WriteLine();
                    Console.WriteLine($"Order {Customer_List[customer_index].Order_List[order_index].OrderId} updated. Changed {Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].ItemName} to {qty}");
                    Console.WriteLine();
                }

                Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].SubTotal = Customer_List[customer_index].Order_List[order_index].OrderedFoodItems_List[itemnumber - 1].CalculateSubTotal();
                Customer_List[customer_index].Order_List[order_index].OrderTotal = Customer_List[customer_index].Order_List[order_index].CalculateOrderTotal() + 5.0;








            }




            if (option == 2)
            {
                while (true)
                {
                    Console.Write("Enter new Address: ");
                    address = Console.ReadLine();
                    if (address != "")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Address cannot be empty");
                    }
                }

                Customer_List[customer_index].Order_List[order_index].DeliveryAddress = address;

                Console.WriteLine();
                Console.WriteLine($"Order {Customer_List[customer_index].Order_List[order_index].OrderId} updated. New Delivery Address: {Customer_List[customer_index].Order_List[order_index].DeliveryAddress}");



            }

            if (option == 3)
            {


                while (true)
                {
                    try
                    {
                        Console.Write("Enter new Delivery Time (hh:mm): ");
                        DateTime deliverytime = Convert.ToDateTime(Console.ReadLine());
                        Order = Customer_List[customer_index].Order_List[order_index];
                        Customer_List[customer_index].Order_List[order_index].DeliveryDateTime = new DateTime(Order.DeliveryDateTime.Year, Order.DeliveryDateTime.Month, Order.DeliveryDateTime.Day, deliverytime.Hour, deliverytime.Minute, Order.DeliveryDateTime.Second);
                        Console.WriteLine();
                        Console.WriteLine($"Order {Customer_List[customer_index].Order_List[order_index].OrderId} updated. New Delivery Time: {Customer_List[customer_index].Order_List[order_index].DeliveryDateTime.Hour}:{Customer_List[customer_index].Order_List[order_index].DeliveryDateTime.Minute}");

                        break;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Please enter time in the correct format");
                    }
                }



            }
            if (option != 1 && option != 2 && option != 3)
            {
                Console.WriteLine("PLease enter a valid option");
                continue;
            }

            break;

        }

        catch (FormatException ex)
        {
            Console.WriteLine("Please only enter a number");
        }

    }







}





LoadRestaurantsAndFoodItems();
LoadCustomersAndOrders();

Console.WriteLine("Welcome to the Gruberoo Food Delivery System");
Console.WriteLine($"{Restaurant_List.Count} restaurants loaded!");
Console.WriteLine($"{FoodItem_List.Count} food items loaded!");
Console.WriteLine($"{Customer_List.Count} customers loaded!");
Console.WriteLine($"{Order_List.Count} orders loaded!");
Console.WriteLine();

while (true)
{
    string option = Display_Gruberoo_Food_deliver_System_Menu();
    if (option == "0")
    {
        break;
    }

    else if (option == "1")
    {
        Display_Restaurants_and_Menu_Items();
    }

    else if (option == "2")
    {
        ListAllOrders();
    }

    else if (option == "3")
    {
        CreateOrder();
    }
    else if (option == "5")
    {
        ModifyOrder();
    }

}



