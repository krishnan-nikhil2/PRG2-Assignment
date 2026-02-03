using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10272831_PRG2Assignment
{
    public class Customer
    {

        private string emailAddress;
        private string customerName;
        public List<Order> Order_List { get; set; } = new List<Order>();

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public Customer(string emailaddress, string customername)
        {
            EmailAddress = emailaddress;
            CustomerName = customername;
        }


        public void AddOrder(Order order)
        {
            Order_List.Add(order);
        }

        public void DisplayAllOrders()
        {
            Console.WriteLine("All Orders");
            Console.WriteLine("==========");
            Console.WriteLine($"{"Order ID",-10} {"Customer",-15} {"Restaurant",-20} {"Delivery Date/Time",-25} {"Amount",-10} {"Status",-10}");
            Console.WriteLine($"{"--------",-10} {"----------",-15} {"-------------",-20} {"------------------",-25} {"------",-10} {"---------",-10}");
            foreach (Order order in Order_List)
            {
                Console.WriteLine($"{order.OrderId,-10} {CustomerName,-15} {order.restaurant.RestaurantName,-20} {order.DeliveryDateTime.ToString("dd/MM/yyyy hh:mm"),-25} {order.CalculateOrderTotal(),-10} {order.OrderStatus,-10}");
            }
        }

        public bool RemoveOrder(Order remove_order)
        {
            bool remove_order_result = false;
            int count = 0;
            foreach (Order order in Order_List)
            {
                if (order.OrderId == remove_order.OrderId)
                {
                    remove_order_result = true;
                    break;
                }
                count += 1;
            }

            if (remove_order_result == true)
            {
                Order_List.RemoveAt(count);
            }

            return remove_order_result;
        }


        public override string ToString()
        {
            return $"EmailAddress: {EmailAddress}, CustomerName: {CustomerName}";
        }


    }
}
