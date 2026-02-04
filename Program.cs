//==========================================================
// Student Number : S10272831G, S10272956J
// Student Names : Krishnan Nikhil, Li Yanjie James
//==========================================================

// 2) Load customers and orders
using Git_Prg2_Assignment;
using System.Collections.Generic;

List<Customer> customers = new List<Customer>();
List<Order> orders = new List<Order>();
void loadCustomerAndOrders(List<Customer> customers, List<Order> orders)
{
    using (StreamReader sr = new StreamReader("customer.csv"))
    {
        //Remove header
        string line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            string name = parts[0];
            string email = parts[1];
            Customer customer = new Customer(email, name);
            customers.Add(customer);
        }
    }
    using (StreamReader sr = new StreamReader("order.csv"))
    {
        string line = sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            //0OrderId,1CustomerEmail,2RestaurantId,3DeliveryDate,4DeliveryTime,5DeliveryAddress,6CreatedDateTime,7TotalAmount,8Status,9Items
            string[] parts = line.Split(',');
            DateTime deliveryDateTime = DateTime.Parse(parts[3] + " " + parts[4]);
            //ASSUMING PAYMENT METHOD IS JUST "Credit Card" SINCE NOT LISTED IN CSV
            //ASSUMING ORDER IS PAID
            Order order = new Order(Convert.ToInt32(parts[0]), DateTime.Parse(parts[6]),
                Convert.ToDouble(parts[7]), parts[8], deliveryDateTime, parts[5], "Credit card", true);
            orders.Add(order);
        }
    }
}

// 4) List all orders with basic info
void ListAllOrders(List<Customer> customers, List<Order> orders)
{
    Console.WriteLine("All Orders");
    Console.WriteLine("==========");
    Console.WriteLine($"{"Order ID",-10} {"Customer",-15} {"Restaurant",-20} {"Delivery Date/Time",-25} {"Amount",-10} {"Status",-10}");
    Console.WriteLine($"{"--------",-10} {"----------",-15} {"-------------",-20} {"------------------",-25} {"------",-10} {"---------",-10}");
    foreach (Order order in orders)
    {
        Customer customer = customers.Find(c => c.EmailAddress == order.Customer.EmailAddress);
        Console.WriteLine($"{order.OrderId,-10} {customer.CustomerName,-15} {order.Restaurant.RestaurantName,-20} {order.DeliveryDateTime.ToString("dd/MM/yyyy hh:mm"),-25} {order.OrderTotal,-10} {order.OrderStatus,-10}");
    }
}

//Test Run
loadCustomerAndOrders(customers, orders);
ListAllOrders(customers, orders);