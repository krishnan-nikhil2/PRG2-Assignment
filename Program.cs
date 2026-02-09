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




    foreach (Restaurant res in Restaurant_List)
    {
        using (StreamReader sr = new StreamReader("fooditems.csv"))
        {
            string line;
            line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {

                string[] fooditem_attributes = line.Split(",");
                FoodItem fooditem = new FoodItem(fooditem_attributes[1], fooditem_attributes[2], Convert.ToDouble(fooditem_attributes[3]), "");
                FoodItem_List.Add(fooditem);
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
            string[] order_attributes = line.Split(",");
            string[] deliverydate_attributes = order_attributes[3].Split("/");
            string[] deliverytime_attributes = order_attributes[4].Split(":");
            string[] orderdate_time = order_attributes[6].Split(" ");
            string[] orderdate_attributes = orderdate_time[0].Split("/");
            string[] ordertime_attributes = orderdate_time[1].Split(":");

            string[] orderedfooditemlist = order_attributes[9].Split("|");

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
            delivery_date = Convert.ToDateTime(string_delivery_date);
            break;
        }

        catch (FormatException ex)
        {
            Console.WriteLine("Please enter the delivery date in the correct format");
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


    Console.Write("Enter Delivery Address: ");
    string delivery_address = Console.ReadLine();

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
            Console.Write("Enter item number (0 to finish):");
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

}

