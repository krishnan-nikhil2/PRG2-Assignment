//==========================================================
// Student Number : S10272831G
// Student Name : Krishnan Nikhil
// Partner Name : Li Yanjie James
//==========================================================


namespace Git_Prg2_Assignment
{
    public class Order
    {

        private int orderId;
        private DateTime orderDateTime;
        private double orderTotal;
        private string orderStatus;
        private DateTime deliveryDateTime;
        private string deliveryAddress;
        private string orderPaymentMethod;
        private bool orderPaid;
        public List<OrderedFoodItem> OrderedFoodItems_List { get; set; } = new List<OrderedFoodItem>();
        public Restaurant restaurant { get; set; }
        public Customer Customer { get; set; }



        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        public DateTime OrderDateTime
        {
            get { return orderDateTime; }
            set { orderDateTime = value; }
        }

        public double OrderTotal
        {
            get { return orderTotal; }
            set { orderTotal = value; }
        }

        public string OrderStatus
        {
            get { return orderStatus; }
            set { orderStatus = value; }
        }


        public DateTime DeliveryDateTime
        {
            get { return deliveryDateTime; }
            set { deliveryDateTime = value; }
        }

        public string DeliveryAddress
        {
            get { return deliveryAddress; }
            set { deliveryAddress = value; }
        }

        public string OrderPaymentMethod
        {
            get { return orderPaymentMethod; }
            set { orderPaymentMethod = value; }
        }

        public bool OrderPaid
        {
            get { return orderPaid; }
            set { orderPaid = value; }
        }

        public Order()
        {

        }

        public Order(int orderid, DateTime orderdatetime, double ordertotal, string orderstatus, 
            DateTime deliverydatetime, string deliveryaddress, string orderpaymentmethod, bool orderpaid)
        {
            OrderId = orderid;
            OrderDateTime = orderdatetime;
            OrderTotal = ordertotal;
            OrderStatus = orderstatus;
            DeliveryDateTime = deliverydatetime;
            DeliveryAddress = deliveryaddress;
            OrderPaymentMethod = orderpaymentmethod;
            OrderPaid = orderpaid;
        }


        public double CalculateOrderTotal()
        {
            foreach (OrderedFoodItem orderedFoodItem in OrderedFoodItems_List)
            {
                OrderTotal += orderedFoodItem.CalculateSubTotal();
            }
            return OrderTotal;
        }

        public void AddOrderedFoodItem(OrderedFoodItem fooditem)
        {
            OrderedFoodItems_List.Add(fooditem);
        }

        public bool RemoveOrderedFoodItem(OrderedFoodItem remove_orderedfooditem)
        {
            bool remove_item = false;
            int count = 0;
            foreach (OrderedFoodItem orderedfoodItem in OrderedFoodItems_List)
            {
                if (orderedfoodItem.ItemName == remove_orderedfooditem.ItemName)
                {
                    remove_item = true;
                    break;
                }
                count += 1;
            }

            if (remove_item == true)
            {
                OrderedFoodItems_List.RemoveAt(count);
            }

            return remove_item;


        }


        public void DisplayOrderedFoodItems()
        {
            foreach (OrderedFoodItem orderedfoodItem in OrderedFoodItems_List)
            {
                Console.WriteLine(orderedfoodItem.ToString());
            }
        }

        public override string ToString()
        {
            return $"OrderId: {OrderId}, OrderDateTime: {OrderDateTime}, OrderTotal: {OrderTotal}, OrderStatus: {OrderStatus}, DeliveryDateTime: {DeliveryDateTime}, DeliveryAddress: {DeliveryAddress}, OrderPaymentMethod: {OrderPaymentMethod}, OrderPaid: {OrderPaid}";
        }





    }
}
